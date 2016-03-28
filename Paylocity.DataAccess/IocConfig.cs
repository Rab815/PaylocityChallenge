using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Paylocity.DataAccess
{
    public static class IocConfig
    {
        public static void RegisterComponents(ContainerBuilder builder)
        {
            builder.RegisterType<EmployeeDAL>().As<IEmployeeDAL>();
            builder.RegisterType<DependentDAL>().As<IDependentDAL>();

            builder.RegisterType<UnitOfWorkFactory>().As<IUnitOfWorkFactory>();
        }
    }
}
