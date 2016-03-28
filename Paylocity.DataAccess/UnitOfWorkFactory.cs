using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paylocity.EF;

namespace Paylocity.DataAccess
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        /// <summary>
        /// Creates the unit of work with the default context.
        /// </summary>
        /// <returns></returns>
        public UnitOfWork CreateUnitOfWork(string contextToUse = "Payroll")
        {
            var unitOfWork = new UnitOfWork();

            switch (contextToUse)
            {
                case "Payroll":
                default:
                    var payrollContext = new PayrollContext();
                    unitOfWork.PayrollContext = payrollContext;
                    unitOfWork.DbContextTransaction = payrollContext.Database.BeginTransaction();
                    break;
            }

            return unitOfWork;
        }

        /// <summary>
        /// Creates the unit of work with the given context.
        /// USED FOR UNIT TESTING DAL ONLY?????.
        /// </summary>
        /// <param name="ctxt">The Entity Framework Context.</param>
        /// <returns></returns>
        internal UnitOfWork CreateUnitOfWork(PayrollContext ctxt)
        {
            var unitOfWork = new UnitOfWork
            {
                PayrollContext = ctxt
            };

            return unitOfWork;
        }

    }
}
