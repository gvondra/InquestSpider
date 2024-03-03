using Autofac;
using InquestSpider.Resource.Framework;

namespace InquestSpider.Resource.Core
{
    public class ResourceCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterModule(new InquestSpider.Resource.Data.ResourceDataModule());
            builder.RegisterType<ResourceExclusionFactory>().As<IResourceExclusionFactory>();
            builder.RegisterType<ResourceExclusionSaver>().As<IResourceExclusionSaver>();
            builder.RegisterType<ResourceFactory>().As<IResourceFactory>();
            builder.RegisterType<ResourceUrlHasher>().SingleInstance();
        }
    }
}
