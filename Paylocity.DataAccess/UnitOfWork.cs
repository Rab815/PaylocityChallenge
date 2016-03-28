using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paylocity.EF;

namespace Paylocity.DataAccess
{
    /// <summary>
    /// Acts as a database transaction manager.
    /// </summary>
    public class UnitOfWork : IDisposable
    {

        internal PayrollContext PayrollContext;
        internal System.Data.Entity.DbContextTransaction DbContextTransaction;

        private bool _disposed = false;
        private bool _completed = false;

        public UnitOfWork()
        {
        }

        /// <summary>
        /// Completes the unit of work transaction and persists all changed data.
        /// </summary>
        public virtual async Task Complete()
        {
            try
            {
                if (PayrollContext != null) await PayrollContext.SaveChangesAsync();

                DbContextTransaction.Commit();

            }
            catch (DbEntityValidationException dbEx)
            {
                DbContextTransaction.Rollback();

                //foreach (
                //    var validationError in
                //        dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors))
                //{
                //    _logger.Error(string.Format("Property: {0} Error: {1}", validationError.PropertyName,
                //        validationError.ErrorMessage));
                //}
                throw;
            }
            _completed = true;
        }

        /// <summary>
        /// Cancels the unit of work transaction and forces all changed data to be rolled back.
        /// </summary>
        public virtual void Cancel()
        {
            DbContextTransaction.Rollback();
            _completed = true;
        }

        /// <summary>
        /// Database context for Payroll Database
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (DbContextTransaction != null)
                    {
                        DbContextTransaction.Dispose();
                    }

                    PayrollContext?.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
