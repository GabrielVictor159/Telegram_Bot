using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.BOT.Application.UseCases.Order.CreateOrder.Handlers;
using Telegram.BOT.Application.Boundaries;
using Telegram.BOT.Application.UseCases.Order.GetOrder.Handlers;

namespace Telegram.BOT.Application.UseCases.Order.GetOrder
{
    public class GetOrderUseCase : IGetOrderRequest
    {
        private readonly IOutputPort<List<Domain.Order.Order>> outputPort;
        private readonly GetOrderHandler getOrderHandler;
        public GetOrderUseCase(
         IOutputPort<List<Domain.Order.Order>> outputPort,
         GetOrderHandler getOrderHandler)
        {
            this.outputPort = outputPort;
            this.getOrderHandler = getOrderHandler;
        }
        public async Task Execute(GetOrderRequest request)
        {
            try
            {
                await getOrderHandler.ProcessRequest(request);
                outputPort.Standard(request.orderResult!);
            }
            catch (Exception ex)
            {
                outputPort.Error(ex.Message);
            }
        }
    }
}