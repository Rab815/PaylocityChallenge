using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paylocity.EF;
using EmployeeModel = Paylocity.Models.EmployeeModel;

namespace Paylocity.DataAccess
{
    public class EmployeeDAL : IEmployeeDAL
    {
        public async Task<List<EmployeeModel>> GetEmployees(UnitOfWork unitOfWork)
        {
            var employees = await unitOfWork.PayrollContext.Employees.ToListAsync();
            // Convert employees from EF to Domain Model better to use automapper
            return employees.Select(employee => new EmployeeModel()
            {
                Id = employee.Id, FirstName = employee.FirstName, LastName = employee.LastName
            }).ToList();
        }


        public async Task<EmployeeModel> SaveEmployee(UnitOfWork unitOfWork, EmployeeModel model)
        {
            try
            {
                var entity = new Employee { Id = model.Id, FirstName = model.FirstName, LastName = model.LastName };

                var trackedEntity =
                    await unitOfWork.PayrollContext.Employees.FirstOrDefaultAsync(x => x.Id == model.Id);

                if (trackedEntity != null)
                {
                    entity.Id = trackedEntity.Id;
                }
                else
                {
                    trackedEntity = unitOfWork.PayrollContext.Employees.Create();
                    unitOfWork.PayrollContext.Employees.Add(trackedEntity);
                }

                unitOfWork.PayrollContext.Entry(trackedEntity).CurrentValues.SetValues(entity);

                await unitOfWork.PayrollContext.SaveChangesAsync();

                return new EmployeeModel()
                {
                    Id = trackedEntity.Id,
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

        public async Task<EmployeeModel> GetEmployee(UnitOfWork unitOfWork, int id)
        {
            var entity = await unitOfWork.PayrollContext.Employees.FirstOrDefaultAsync(c => c.Id == id);
            // Convert employees from EF to Domain Model better to use automapper
            return new EmployeeModel()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName
            };
        }
    }
}
