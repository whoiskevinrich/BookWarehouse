using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoMapper;
using BookWarehouse.Core;
using BookWarehouse.Core.Data;
using BookWarehouse.Core.Infrastructure;
using BookWarehouse.Core.Service;
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

            RegisterAutomapper();
            RegisterLoggers();

            Builder.RegisterFilterProvider();
            
            var container = Builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var profiles = container.Resolve<IEnumerable<Profile>>();
                foreach (var profile in profiles)
                {
                    Mapper.Initialize(cfg =>
                    {
                        cfg.AddProfile(profile);
                    });
                }
            }

                DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void RegisterLoggers()
        {
            Builder.RegisterAssemblyTypes(typeof (DbLogger).Assembly)
                .Where(x => x.Name.EndsWith("Logger"))
                .As<ILogger>();
        }

        private static void RegisterAutomapper()
        {
            Builder.RegisterAssemblyTypes(typeof (AutofacConfig).Assembly)
                .Where(x => x.IsSubclassOf(typeof (Profile)))
                .As<Profile>();
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
