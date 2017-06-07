using Autofac;
using PublishingHouse.Services.Algorithm;
using PublishingHouse.Services.Algorithm.Interface;
using PublishingHouse.Services.Service;
using PublishingHouse.Services.Service.Interface;

namespace PublishingHouse.Services
{
    public class ServicesDependencyBuilder: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // The generic ILogger<TCategoryName> service was added to the ServiceCollection by ASP.NET Core.
            // It was then registered with Autofac using the Populate method in ConfigureServices.
            //builder.RegisterType<SimplifiedNGramService>().As<ISearchService>();
            //builder.RegisterType<NGramService>().As<ISearchService>();
            builder.RegisterType<NGramAlgorithm>().As<INGramAlgorithm>();
            builder.RegisterType<SignatureService>().As<ISearchService>();
            builder.RegisterType<SignatureAlgorithm>().As<ISignatureAlgorithm>();
        }
    }
}
