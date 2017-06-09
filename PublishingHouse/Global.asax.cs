using Autofac;
using Autofac.Integration.Mvc;
using PublishingHouse.Data;
using PublishingHouse.Data.Entities;
using PublishingHouse.SearchServices;
using PublishingHouse.Services;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PublishingHouse
{
    public class MvcApplication : HttpApplication
    {
        internal static IContainer container { get; private set; }
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<DataContext>().AsSelf().InstancePerRequest();

            // MVC - OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();

            // MVC - OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // MVC - OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());


            // You can register controllers all at once using assembly scanning...
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModule(new DataDependencyBuilder());
            builder.RegisterModule(new ServicesDependencyBuilder());
            builder.RegisterType<CustomizedSearchService>().As<ICustomizedSearchService>();

            container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
