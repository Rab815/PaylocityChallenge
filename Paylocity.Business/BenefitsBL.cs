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
        public BenefitsBL(IUnitOfWorkFactory unitOfWorkFactory, IEmployeeDAL employeeDal)
        {
            _employeeDal = employeeDal;
            _dependentsDal = _dependentsDal;
            _unitOfWorkFactory = unitOfWorkFactory;
        }
        public async Task<BenefitsModel> GetEmployeeBenefits(int employeeId)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                //var employee = await _employeeDal.GetEmployee(unitOfWork, employeeId);

                var dependents = await _dependentsDal.GetDependents(unitOfWork, employeeId);
                var depCount = dependents.Count();

                CalculateBenefitsAdjustment(depCount);
            }
            return null;
        }

        private void CalculateBenefitsAdjustment(int depCount)
        {
            //< add key = "NumberOfChecks" value = "26" />
            //< add key = "EmployeeSalaryPaycheck" value = "2000" />
            //< add key = "EmployeeyearlyBenefitCost" value = "1000" />
            //< add key = "DependentYearlyBenefitCost" value = "500" />
            //< add key = "DiscountAmount" value = "10" />
            //< add key = "Discount" value = "24" />

            var numChecks = ConfigurationManager.AppSettings["NumberOfChecks"];
        }
    }
}
