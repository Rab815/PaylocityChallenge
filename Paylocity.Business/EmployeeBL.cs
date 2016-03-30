using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paylocity.DataAccess;
using Paylocity.EF;
using Paylocity.Models;

namespace Paylocity.Business
{
    public class EmployeeBL : IEmployeeBL
    {
        private readonly IEmployeeDAL _employeeDal;
        private readonly IDependentDAL _dependentDal;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        public EmployeeBL(IUnitOfWorkFactory unitOfWorkFactory, IEmployeeDAL employeeDal, IDependentDAL dependentDal)
        {
            _employeeDal = employeeDal;
            _dependentDal = dependentDal;
            _unitOfWorkFactory = unitOfWorkFactory;
        }
        public async Task<EmployeeModel> GetEmployee(int id)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                return await _employeeDal.GetEmployee(unitOfWork, id);
            }
        }

        public async Task<List<EmployeeModel>> GetEmployees()
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                return await _employeeDal.GetEmployees(unitOfWork);
            }
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                // delete all the dependents associated with the employee first
                var isDeleted = await _dependentDal.DeleteDependentsByEmployeeId(unitOfWork, id);
                if (!isDeleted)
                    return false;
                await _employeeDal.DeleteEmployee(unitOfWork, id);
                //complete the transaction
                await unitOfWork.Complete();
                return true;
            }
        }

        public async Task<EmployeeModel> SaveEmployee(EmployeeModel employee)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var retval = await _employeeDal.SaveEmployee(unitOfWork, employee);
                await unitOfWork.Complete();
                return retval;
            }
            
        }
    }
}
