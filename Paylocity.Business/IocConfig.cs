using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Paylocity.Business
{
    public static class IocConfig
    {
        public static void RegisterComponents(ContainerBuilder builder)
        {
            builder.RegisterType<EmployeeBL>().As<IEmployeeBL>();
            builder.RegisterType<DependentBL>().As<IDependentBL>();
            builder.RegisterType<BenefitsBL>().As<IBenefitsBL>();

            AddDataComponents(builder);
        }

        private static void AddDataComponents(ContainerBuilder builder)
        {
            DataAccess.IocConfig.RegisterComponents(builder);
        }
    }
}
