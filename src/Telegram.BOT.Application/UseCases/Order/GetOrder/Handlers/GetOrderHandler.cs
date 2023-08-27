using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases;
using Telegram.BOT.Application.UseCases.Order.GetOrder;

namespace Telegram.BOT.Application.UseCases.Order.GetOrder.Handlers
{
    public class GetOrderHandler : Handler<GetOrderRequest>
    {
        private readonly IOrderRepository _orderRepository;
        public GetOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public override async Task ProcessRequest(GetOrderRequest request)
        {
            var result = await _orderRepository.GetOrder(request.func);
            request.SetOutput(result);
            sucessor?.ProcessRequest(request);
        }
    }
}