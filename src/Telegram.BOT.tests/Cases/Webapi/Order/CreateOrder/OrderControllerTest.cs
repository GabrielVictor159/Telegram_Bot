using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Telegram.BOT.WebApi.UseCases;
using Microsoft.AspNetCore.Mvc;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.WebApi.UseCases.Order.CreateOrder;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Webapi.Order.CreateOrder
{
    [UseAutofacTestFramework]
    public class OrderControllerTest
    {
        private readonly OrderController createOrderController;
        private readonly INotificationService notificationService;
        public OrderControllerTest(
            OrderController createOrderController,
            INotificationService notificationService)
        {
            this.createOrderController = createOrderController;
            this.notificationService = notificationService;            
        }
        [Fact]
        public async Task ShouldExecuteCreateWithSucess()
        {
            var result = await createOrderController.CreateOrder(new WebApi.UseCases.Order.CreateOrder.CreateOrderRequest(){OrderDate =DateTime.Today, TotalOrder = 1000});
            notificationService.HasNotifications.Should().BeFalse();
        }
        [Fact]
        public async Task ShouldExecuteCreateWithFailed()
        {
            var result = await createOrderController.CreateOrder(new WebApi.UseCases.Order.CreateOrder.CreateOrderRequest(){OrderDate =DateTime.Today.AddDays(2), TotalOrder = 0});
            notificationService.HasNotifications.Should().BeTrue();
        }
    }
}