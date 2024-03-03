using Autofac;

namespace ResourceRPC
{
    public class ResourceRpcModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterModule(new InquestSpider.Resource.Core.ResourceCoreModule());
            builder.RegisterModule(new InquestSpider.Interface.Resource.ResourceInterfaceModule());

            builder.RegisterType<SettingsFactory>().InstancePerLifetimeScope();
        }
    }
}
