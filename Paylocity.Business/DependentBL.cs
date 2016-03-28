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
    public class DependentBL : IDependentBL
    {
        private readonly IDependentDAL _dependentDal;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        public DependentBL(IUnitOfWorkFactory unitOfWorkFactory, IDependentDAL dependentDal)
        {
            _dependentDal = dependentDal;
            _unitOfWorkFactory = unitOfWorkFactory;
        }
        public async Task<DependentModel> GetDependent(int id)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                return await _dependentDal.GetDependent(unitOfWork, id);
            }
        }

        public async Task<List<DependentModel>> GetDependents(int employeeId)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                return await _dependentDal.GetDependents(unitOfWork, employeeId);
            }
        }

        public async Task<DependentModel> SaveDependent(DependentModel Dependent)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var retval = await _dependentDal.SaveDependent(unitOfWork, Dependent);
                await unitOfWork.Complete();
                return retval;
            }
            
        }

        public async Task<bool> DeleteDependent(int id)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var isDeleted = await _dependentDal.DeleteDependent(unitOfWork, id);
                await unitOfWork.Complete();
                return isDeleted;
            }
        }
    }
}
