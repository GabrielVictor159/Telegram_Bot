using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Infrastructure.Database;
using Telegram.BOT.tests.Builder.Domain;
using Telegram.BOT.Application.UseCases.Order.CreateOrder;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Application.Order.CreateOrder
{
    [UseAutofacTestFramework]
    public class CreateOrderUseCaseTest
    {
        public readonly ICreateOrderRequest createOrderUseCase;
        public CreateOrderUseCaseTest(
            ICreateOrderRequest createOrderUseCase)
        {
            this.createOrderUseCase = createOrderUseCase;
        } 
        [Fact]
        public async Task ShouldCreateOrderUseCaseExecuteDomainValid()
        {
            var request = new CreateOrderRequest(OrderBuilder.New().Build());
            await createOrderUseCase.Execute(request);
            request.OrderOutput.Should().NotBeNull();
        }
    }
}