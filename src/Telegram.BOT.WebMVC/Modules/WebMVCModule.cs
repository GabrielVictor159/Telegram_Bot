using Autofac;
using Telegram.BOT.Infrastructure;

namespace Telegram.BOT.WebMVC.Modules
{
    public class WebMVCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(WebMVCException).Assembly)
            .AsImplementedInterfaces().AsSelf().InstancePerLifetimeScope();
        }
    }
}
