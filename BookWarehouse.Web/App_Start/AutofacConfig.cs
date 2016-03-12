using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using BookWarehouse.Core;
using BookWarehouse.Core.Data;
using BookWarehouse.Core.Infrastructure;
using BookWarehouse.Service;
using BookWarehouse.Web.Controllers.Api;

namespace BookWarehouse.Web
{
    public class AutofacConfig
    {
        public static ContainerBuilder Builder;

        public static void RegisterDependencies()
        {
            Builder = new ContainerBuilder();

            RegisterMvcComponents();
            RegisterWebApiComponents();

            RegisterWarehouseContext();
            RegisterRepositoryPatterns();
            RegisterServicesByConvention();
            
            Builder.RegisterFilterProvider();

            var container = Builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void RegisterWebApiComponents()
        {
            Builder.RegisterApiControllers(typeof(WarehouseApiController).Assembly);
        }

        private static void RegisterMvcComponents()
        {
            Builder.RegisterControllers(typeof(MvcApplication).Assembly).InstancePerDependency();

            Builder.RegisterFilterProvider();
            Builder.RegisterAssemblyModules(typeof(MvcApplication).Assembly);
            Builder.RegisterModule<AutofacWebTypesModule>();
        }

        private static void RegisterServicesByConvention()
        {
            Builder.RegisterAssemblyTypes(typeof (WarehouseService).Assembly)
                .Where(x => x.Name.EndsWith("Service"))
                .AsImplementedInterfaces();
        }

        private static void RegisterRepositoryPatterns()
        {
            Builder.RegisterGeneric(typeof (WarehouseRepository<>))
                .As(typeof (IRepository<>));
        }

        private static void RegisterWarehouseContext()
        {
            Builder.RegisterType<WarehouseContext>().AsSelf().InstancePerRequest();
        }
    }
}
