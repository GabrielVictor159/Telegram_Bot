using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Telegram.BOT.Application.UseCases.Order.CreateOrder;
using Telegram.BOT.tests.Builder.Domain;
using Telegram.BOT.Application.UseCases.Order.CreateOrder.Handlers;
using Telegram.BOT.Infrastructure.Database;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Application.Order.CreateOrder.Handlers
{
    [UseAutofacTestFramework]
    public class SaveOrderHandlerTests
    {
        private readonly SaveOrderHandler saveOrderHandler;
        private readonly Context context;
        public SaveOrderHandlerTests(SaveOrderHandler saveOrderHandler, Context context)
        {
            this.saveOrderHandler =saveOrderHandler;
            this.context = context; 
        }
        [Fact]
        public async void ShouldSaveOrderHandlerDomainValid()
        {
            var request = new CreateOrderRequest(OrderBuilder.New().Build());
            await saveOrderHandler.ProcessRequest(request);
            var result = context.Orders.Find(request.Order.Id);
            result.Should().NotBeNull();
        }
    }
}