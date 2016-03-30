using Paylocity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.Business
{
    public interface IDependentBL
    {
        Task<DependentModel> GetDependent(int id);
        Task<List<DependentModel>> GetDependents(int employeeId);
        Task<DependentModel> SaveDependent(DependentModel employee);
        Task<bool> DeleteDependent(int id);
        Task<bool> DeleteDependentsByEmployeeId(int employeeId);

    }
}
