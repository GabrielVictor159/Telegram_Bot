using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.BOT.TelegramJob.Modules
{
    public class JobModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(JobException).Assembly)
            .AsImplementedInterfaces().AsSelf().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
