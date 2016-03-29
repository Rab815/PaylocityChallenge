using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paylocity.Models;

namespace Paylocity.DataAccess
{
    public interface IEmployeeDAL
    {
        Task<List<EmployeeModel>> GetEmployees(UnitOfWork unitOfWork);
        Task<EmployeeModel> SaveEmployee(UnitOfWork unitOfWork, EmployeeModel model);
        Task<EmployeeModel> GetEmployee(UnitOfWork unitOfWork, int id);
        Task<bool> DeleteEmployee(UnitOfWork unitOfWork, int id);
    }
}
