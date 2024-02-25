using Autofac;

namespace InquestSpider.Resource.Core
{
    public class ResourceCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterModule(new InquestSpider.Resource.Data.ResourceDataModule());
            builder.RegisterType<ResourceUrlHasher>().SingleInstance();
        }
    }
}
