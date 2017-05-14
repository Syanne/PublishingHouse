using Autofac;
using PublishingHouse.Data;
using PublishingHouse.Services;
using PublishingHouse.Data.Repository.Interface;
using PublishingHouse.Services.Algorithm.Interface;
using System;
using PublishingHouse.Services.Service.Interface;
using PublishingHouse.Data.Entities;

namespace PublishingHouse.UnitTest
{
    public class TestDependencies
    {
        private IContainer _autofacContainer;
        protected IContainer AutofacContainer
        {
            get
            {
                if (_autofacContainer == null)
                {
                    var builder = new ContainerBuilder();

                    builder.RegisterType<DataContext>().AsSelf().InstancePerLifetimeScope();

                    builder.RegisterModule<DataDependencyBuilder>();
                    builder.RegisterModule<ServicesDependencyBuilder>();

                    var container = builder.Build();

                    _autofacContainer = container;
                }

                return _autofacContainer;
            }
        }

        protected ISearchService SearchService
        {
            get
            {
                return AutofacContainer.Resolve<ISearchService>();
            }
        }
        protected INGramAlgorithm NGramAlgorithm
        {
            get
            {
                return AutofacContainer.Resolve<INGramAlgorithm>();
            }
        }

        protected IArticleWriteRepository ArticleWriteRepository
        {
            get
            {
                return AutofacContainer.Resolve<IArticleWriteRepository>();
            }
        }
        protected IArticleReadRepository ArticleReadRepository
        {
            get
            {
                return AutofacContainer.Resolve<IArticleReadRepository>();
            }
        }
        protected INGramsReadRepository NGramsReadRepository
        {
            get
            {
                return AutofacContainer.Resolve<INGramsReadRepository>();
            }
        }
        protected INGramsWriteRepository NGramsWriteRepository
        {
            get
            {
                return AutofacContainer.Resolve<INGramsWriteRepository>();
            }
        }

    }
}
