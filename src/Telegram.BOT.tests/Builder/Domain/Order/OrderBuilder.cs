using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Order;

namespace Telegram.BOT.tests.Builder.Domain
{
    public class OrderBuilder
    {
        public Guid Id;
        public DateTime OrderDate;
        public decimal TotalOrder;
        public static OrderBuilder New()
        {
            return new OrderBuilder()
            {
                Id = Guid.NewGuid(),
                OrderDate = DateTime.Now,
                TotalOrder = 1000
            };
        }
        public OrderBuilder WithId(Guid id)
        {
            Id = id;
            return this;
        }
        public OrderBuilder WithOrderDate(DateTime orderDate)
        {
            OrderDate = orderDate;
            return this;
        }
        public OrderBuilder WithTotalOrder(decimal totalOrder)
        {
            TotalOrder = totalOrder;
            return this;
        }
        public Order Build()
        => new Order(Id,OrderDate,TotalOrder);
    }
}