using Autofac;
using Autofac.Integration.Mvc;
using TNVCMS.Data.DataAcess;
using TNVCMS.Data.DatabaseModel;
using System.Web.Mvc;
using TNVCMS.Domain.Services;

namespace TNVCMS.Web.App_Start
{
    public class DependencyConfig
    {
        public static void Configure(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(T_NewsServices).Assembly).AsImplementedInterfaces().InstancePerHttpRequest();
            builder.RegisterAssemblyTypes(typeof(T_TagServices).Assembly).AsImplementedInterfaces().InstancePerHttpRequest();

            builder.RegisterControllers(typeof(MvcApplication).Assembly)
                   .PropertiesAutowired();

            builder.RegisterAssemblyTypes(typeof(ContextAdaptor).Assembly)
                .AsImplementedInterfaces()
                .InstancePerHttpRequest();

            builder.RegisterType<TNVCMSEntities>()
                .As<IDbContext>()
                .InstancePerHttpRequest();

            builder.RegisterType<ContextAdaptor>()
                .As<IObjectSetFactory, IObjectContext>()
                .InstancePerHttpRequest();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerHttpRequest();


            builder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .InstancePerHttpRequest();

            var container = builder.Build();
            var dependencyResolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(dependencyResolver);
        }
    }
}