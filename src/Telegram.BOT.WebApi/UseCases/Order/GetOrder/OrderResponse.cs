using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Telegram.BOT.WebApi.UseCases.Order.GetOrder
{
    public class OrderResponse
    {
        public List<Domain.Order.Order> orders { get; private set; }
        public OrderResponse(List<Domain.Order.Order> orders)
        {
            this.orders = orders;
        }
    }
}