using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.DataAccess
{
    public interface IUnitOfWorkFactory
    {
        UnitOfWork CreateUnitOfWork(string contextToUse = "Payroll");
    }
}
