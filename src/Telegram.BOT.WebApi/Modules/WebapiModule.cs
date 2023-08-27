using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Telegram.BOT.WebApi.UseCases.Order.CreateOrder;
using Telegram.BOT.WebApi.UseCases.Order.GetOrder;
using Telegram.BOT.WebApi.UseCases.Order.RemoveOrder;

namespace Telegram.BOT.WebApi.Modules
{
    public class WebapiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CreateOrderPresenter>()
             .AsImplementedInterfaces()
             .InstancePerLifetimeScope().AsSelf();
            builder.RegisterType<GetOrderPresenter>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope().AsSelf();
            builder.RegisterType<RemoveOrderPresenter>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope().AsSelf();
        }
    }
}