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
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        public EmployeeBL(IUnitOfWorkFactory unitOfWorkFactory, IEmployeeDAL employeeDal)
        {
            _employeeDal = employeeDal;
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
                var isDeleted = await _employeeDal.DeleteEmployee(unitOfWork, id);
                await unitOfWork.Complete();
                return isDeleted;
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
