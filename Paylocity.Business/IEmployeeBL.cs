using Paylocity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.Business
{
    public interface IEmployeeBL
    {
        Task<EmployeeModel> GetEmployee(int id);
        Task<List<EmployeeModel>> GetEmployees();
        Task<EmployeeModel> SaveEmployee(EmployeeModel employee);
        Task<bool> DeleteEmployee(int id);

    }
}
