using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Application.Boundaries;
using Telegram.BOT.Application.UseCases.Order.RemoveOrder.Handlers;

namespace Telegram.BOT.Application.UseCases.Order.RemoveOrder
{
    public class RemoveOrderUseCase : IRemoveOrderRequest
    {
        private readonly IOutputPort<string> outputPort;
        private readonly RemoveOrderHandler removeOrderHandler;
        public RemoveOrderUseCase(
         IOutputPort<string> outputPort,
         RemoveOrderHandler removeOrderHandler)
        {
            this.outputPort = outputPort;
            this.removeOrderHandler = removeOrderHandler;
        }
        public async Task Execute(RemoveOrderRequest request)
        {
            try
            {
                await removeOrderHandler.ProcessRequest(request);
                outputPort.Standard(request.orderResult!);
            }
            catch (Exception ex)
            {
                outputPort.Error(ex.Message);
            }
        }
    }
}