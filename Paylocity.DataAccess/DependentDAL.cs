using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paylocity.EF;
using Paylocity.Models;
using EmployeeModel = Paylocity.Models.EmployeeModel;

namespace Paylocity.DataAccess
{
    public class DependentDAL : IDependentDAL
    {
        public async Task<List<DependentModel>> GetDependents(UnitOfWork unitOfWork, int employeeId)
        {
            var dependents = await unitOfWork.PayrollContext.Dependents.Where(d=> d.EmployeeId == employeeId).ToListAsync();
            // Convert employees from EF to Domain Model better to use automapper
           return dependents.Select(dependent => new DependentModel()
            {
                Id = dependent.Id, EmployeeId = dependent.EmployeeId,  FirstName = dependent.FirstName, LastName = dependent.LastName
            }).ToList();
        }


        public async Task<DependentModel> SaveDependent(UnitOfWork unitOfWork, DependentModel model)
        {
            try
            {
                var entity = new Dependent { Id = model.Id, EmployeeId = model.EmployeeId, FirstName = model.FirstName, LastName = model.LastName };

                var trackedEntity =
                    await unitOfWork.PayrollContext.Dependents.FirstOrDefaultAsync(x => x.Id == model.Id);

                if (trackedEntity != null)
                {
                    entity.Id = trackedEntity.Id;
                }
                else
                {
                    trackedEntity = unitOfWork.PayrollContext.Dependents.Create();
                    unitOfWork.PayrollContext.Dependents.Add(trackedEntity);
                }

                unitOfWork.PayrollContext.Entry(trackedEntity).CurrentValues.SetValues(entity);

                await unitOfWork.PayrollContext.SaveChangesAsync();

                return new DependentModel()
                {
                    Id = trackedEntity.Id,
                    EmployeeId = trackedEntity.EmployeeId,
                    FirstName = trackedEntity.FirstName,
                    LastName = trackedEntity.LastName
                };
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                throw ;
            }

        }

        public async Task<bool> DeleteDependent(UnitOfWork unitOfWork, int id)
        {
            var entity = await unitOfWork.PayrollContext.Dependents.FirstOrDefaultAsync(c => c.Id == id);
            if (entity != null)
            {
                unitOfWork.PayrollContext.Dependents.Remove(entity);
                await unitOfWork.PayrollContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<DependentModel> GetDependent(UnitOfWork unitOfWork, int id)
        {
            var entity = await unitOfWork.PayrollContext.Dependents.FirstOrDefaultAsync(c => c.Id == id);
            // Convert dependents from EF to Domain Model better to use automapper
            return new DependentModel()
            {
                Id = entity.Id,
                EmployeeId = entity.EmployeeId,
                FirstName = entity.FirstName,
                LastName = entity.LastName
            };
        }
    }
}
