using Autofac;
using Telegram.BOT.Infrastructure.Modules;
using Telegram.BOT.WebMVC.Modules;

namespace Telegram.BOT.WebMVC.DependencyInjection
{
    public static class AutofacExtensions
    {
        public static ContainerBuilder AddAutofacRegistration(this ContainerBuilder builder)
        {
            builder.RegisterModule(new WebMVCModule());
            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterModule(new ApplicationModule());
            return builder;
        }
    }
}
