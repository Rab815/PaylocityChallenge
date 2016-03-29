using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paylocity.DataAccess;
using Paylocity.Models;

namespace Paylocity.Business
{
    public class BenefitsBL : IBenefitsBL
    {

        private readonly IEmployeeDAL _employeeDal;
        private readonly IDependentDAL _dependentsDal;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        public BenefitsBL(IUnitOfWorkFactory unitOfWorkFactory, IEmployeeDAL employeeDal, IDependentDAL dependentsDal)
        {
            _employeeDal = employeeDal;
            _dependentsDal = dependentsDal;
            _unitOfWorkFactory = unitOfWorkFactory;
        }
        public async Task<BenefitsModel> GetEmployeeBenefits(int employeeId)
        {
            BenefitsModel model = null;
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var employee = await _employeeDal.GetEmployee(unitOfWork, employeeId);

                var dependents = await _dependentsDal.GetDependents(unitOfWork, employeeId);

                 model = CalculateBenefitsAdjustment(employee, dependents);
            }
            return model;
        }

        private BenefitsModel CalculateBenefitsAdjustment(EmployeeModel employee, List<DependentModel> dependents)
        {
            // Get constants from web.config
            var numChecks = GetSetting<int>("NumberOfChecks");
            var empSalaryPerPay = GetSetting<double>("EmployeeSalaryPaycheck");
            var empBenfitCost = GetSetting<double>("EmployeeYearlyBenefitCost");
            var depBenefitCost = GetSetting<double>("DependentYearlyBenefitCost");
            var discountPercent = GetSetting<double>("DiscountAmountPercentage");
            var discountNameStart = GetSetting<string>("DiscountNameStart");
            // count the dependents
            var depCount = dependents.Count();

            // determine how much per check comes out for the employee
            var employeeBenefitsCostPerPaycheck = empBenfitCost/numChecks;
            var dependantsBenefitCostPerPaycheck = depBenefitCost/numChecks;
            
            // round the values
            employeeBenefitsCostPerPaycheck = Math.Round(employeeBenefitsCostPerPaycheck, 2);
            dependantsBenefitCostPerPaycheck = Math.Round(dependantsBenefitCostPerPaycheck, 2);

            // check for the discount condition
            double? employeeDiscountedBenefitAmount = null;
            
            //does the employee get a discount
            if (employee?.FirstName.StartsWith(discountNameStart) == true)
            {
                employeeDiscountedBenefitAmount = employeeBenefitsCostPerPaycheck * (discountPercent/100);
            }

            // how many dependents get a discount
            var discountedDependentsCount = dependents.Count(d => d.FirstName.StartsWith(discountNameStart));
            
            var discountedDependantsBenefitCostPerPaycheck = dependantsBenefitCostPerPaycheck - ((dependantsBenefitCostPerPaycheck * (discountPercent / 100)) * discountedDependentsCount);
            var nondiscountedDependantBenefitCostPerPaycheck = (depCount - discountedDependentsCount) * dependantsBenefitCostPerPaycheck;

            // calculate the total discount
            var totalDeductions = (employeeDiscountedBenefitAmount ?? employeeBenefitsCostPerPaycheck) + (discountedDependantsBenefitCostPerPaycheck + nondiscountedDependantBenefitCostPerPaycheck);

            // round values
            totalDeductions = Math.Round(totalDeductions, 2);
            var netPay = empSalaryPerPay - totalDeductions;

            return new BenefitsModel
            {
                GrossPay = empSalaryPerPay,
                NetPay = netPay,
                NetDeductions = totalDeductions
            };

        }

        // Normally this would be a part of a utility class... here for brevity
        public static T GetSetting<T>(string key, T defaultValue = default(T)) where T : IConvertible
        {
            string val = ConfigurationManager.AppSettings[key] ?? "";
            T result = defaultValue;
            if (!string.IsNullOrEmpty(val))
            {
                T typeDefault = default(T);
                if (typeof(T) == typeof(String))
                {
                    typeDefault = (T)(object)String.Empty;
                }
                result = (T)Convert.ChangeType(val, typeDefault.GetTypeCode());
            }
            return result;
        }
    }
}
