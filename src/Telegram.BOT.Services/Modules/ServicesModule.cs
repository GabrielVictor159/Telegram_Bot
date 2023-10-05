using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.BOT.Services.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ServicesException).Assembly)
            .AsImplementedInterfaces().AsSelf().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}