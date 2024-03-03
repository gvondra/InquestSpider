using Autofac;
using BrassLoon.DataClient.MongoDB;
using InquestSpider.Resource.Data.Internal;

namespace InquestSpider.Resource.Data
{
    public class ResourceDataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<DbProvider>()
                .SingleInstance()
                .As<IDbProvider>();
            builder.RegisterType<ResourceDataService>().As<IResourceDataService>();
            builder.RegisterType<ResourceExclusionDataService>().As<IResourceExclusionDataService>();
        }
    }
}
