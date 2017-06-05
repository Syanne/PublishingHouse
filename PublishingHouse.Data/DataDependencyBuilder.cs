using Autofac;
using PublishingHouse.Data.Repository;
using PublishingHouse.Data.Repository.Interface;

namespace PublishingHouse.Data
{
    public class DataDependencyBuilder : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // The generic ILogger<TCategoryName> service was added to the ServiceCollection by ASP.NET Core.
            // It was then registered with Autofac using the Populate method in ConfigureServices.containerBuilder

            builder.RegisterType<ArticleReadRepository>()
                .As<IArticleReadRepository>();

            builder.RegisterType<ArticleWriteRepository>()
                .As<IArticleWriteRepository>();

            builder.RegisterType<NGramsReadRepository>()
                .As<INGramsReadRepository>();

            builder.RegisterType<NGramsWriteRepository>()
                .As<INGramsWriteRepository>();

            builder.RegisterType<SignatureReadRepository>()
                .As<ISignatureReadRepository>();

            builder.RegisterType<SignatureWriteRepository>()
                .As<ISignatureWriteRepository>();
        }
    }
}
