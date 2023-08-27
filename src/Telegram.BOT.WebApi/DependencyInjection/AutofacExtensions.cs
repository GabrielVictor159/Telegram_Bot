using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Telegram.BOT.Infrastructure.Modules;
using Telegram.BOT.WebApi.Modules;

namespace Telegram.BOT.WebApi.DependencyInjection
{
    public static class AutofacExtensions
    {
        public static ContainerBuilder AddAutofacRegistration(this ContainerBuilder builder)
        {
            builder.RegisterModule(new WebapiModule());
            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterModule(new ApplicationModule());
            return builder;
        }
    }
}