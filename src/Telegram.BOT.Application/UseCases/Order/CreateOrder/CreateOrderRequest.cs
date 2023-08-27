using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.BOT.Application.Boundaries.Order;

namespace Telegram.BOT.Application.UseCases.Order.CreateOrder
{
    public class CreateOrderRequest
    {
        public Domain.Order.Order Order { get; private set; }
        public OrderOutput? OrderOutput { get; private set; }
        public CreateOrderRequest(Domain.Order.Order order)
        {
            Order = order;
        }

        public void SetOutput(Guid id)
         => OrderOutput = new OrderOutput(id);
    }
}