using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;

namespace Telegram.BOT.Infrastructure.Modules
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(Application.ApplicationException).Assembly)
            .AsImplementedInterfaces().AsSelf().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}