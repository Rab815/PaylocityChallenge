using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;

namespace Paylocity.Payroll.WebApi.App_Start
{
    public static class IocConfig
    {
        public static void Initialize()
        {
            var builder = new ContainerBuilder();
            
            Business.IocConfig.RegisterComponents(builder);

            // Register the Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register the MVC controllers
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            // Build the container.
            var container = builder.Build();

            // Create the depenedency resolver.
            var webApiResolver = new AutofacWebApiDependencyResolver(container);
            var httpResolver = new AutofacDependencyResolver(container);

            // Set the application resolvers
            GlobalConfiguration.Configuration.DependencyResolver = webApiResolver;
            DependencyResolver.SetResolver(httpResolver);
        }
    }
}