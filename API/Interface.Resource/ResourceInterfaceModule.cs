using Autofac;

namespace InquestSpider.Interface.Resource
{
    public class ResourceInterfaceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<ResourceExclusionService>().As<IResourceExclusionService>();
            builder.RegisterType<ResourceService>().As<IResourceService>();
        }
    }
}
