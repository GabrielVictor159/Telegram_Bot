using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Application.Boundaries;
using Telegram.BOT.Application.Boundaries.Order;
using Telegram.BOT.Application.UseCases.Order.CreateOrder.Handlers;

namespace Telegram.BOT.Application.UseCases.Order.CreateOrder
{
    public class CreateOrderUseCase : ICreateOrderRequest
    {
        private readonly ValidateOrderHandler validateOrderHandler;
        private readonly IOutputPort<OrderOutput> outputPort;
        public CreateOrderUseCase(
         ValidateOrderHandler validateOrderHandler,
         SaveOrderHandler saveOrderHandler,
         IOutputPort<OrderOutput> outputPort)
        {
            validateOrderHandler.SetSucessor(saveOrderHandler);
            this.validateOrderHandler = validateOrderHandler;
            this.outputPort = outputPort;

        }
        public async Task Execute(CreateOrderRequest request)
        {
            try
            {
                await validateOrderHandler.ProcessRequest(request);
                outputPort.Standard(new OrderOutput(request.Order.Id));
            }
            catch (Exception ex)
            {
                outputPort.Error(ex.Message);
            }
        }
    }
}