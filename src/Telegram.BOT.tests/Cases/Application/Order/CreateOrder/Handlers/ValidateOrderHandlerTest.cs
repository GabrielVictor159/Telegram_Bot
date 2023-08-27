using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Telegram.BOT.Application.UseCases.Order.CreateOrder;
using Telegram.BOT.tests.Builder.Domain;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Application.UseCases.Order.CreateOrder.Handlers;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Application.Order.CreateOrder.Handlers
{
    [UseAutofacTestFramework]
    public class ValidateOrderHandlerTest
    {
        private readonly ValidateOrderHandler validateOrderHandler;
        private readonly INotificationService notificationService;
        public ValidateOrderHandlerTest(
         ValidateOrderHandler validateOrderHandler,
         INotificationService notificationService)
        {
            this.validateOrderHandler = validateOrderHandler;
            this.notificationService = notificationService;
        }
        [Fact]
        public async Task ShouldHaveNotificationWhenDomainInvalid()
        {
            var request = new CreateOrderRequest(OrderBuilder.New().WithTotalOrder(0).Build());
            await validateOrderHandler.ProcessRequest(request);
            notificationService.HasNotifications.Should().BeTrue();
        }
    }
}