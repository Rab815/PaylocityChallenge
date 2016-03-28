using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paylocity.Models;

namespace Paylocity.DataAccess
{
    public interface IDependentDAL
    {
        Task<List<DependentModel>> GetDependents(UnitOfWork unitOfWork, int employeeId);
        Task<DependentModel> SaveDependent(UnitOfWork unitOfWork, DependentModel model);
        Task<DependentModel> GetDependent(UnitOfWork unitOfWork, int id);
        Task<bool> DeleteDependent(UnitOfWork unitOfWork, int id);
    }
}
