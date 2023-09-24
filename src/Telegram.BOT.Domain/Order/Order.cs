using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Validator.Order;

namespace Telegram.BOT.Domain.Order
{
    public class Order : Entity <Order, OrderValidator>
    {
        public Guid Id { get; init; }
        public DateTime OrderDate { get; init; }
        public decimal TotalOrder { get; init; }

        public Order()
            : base(new OrderValidator())
        {
        }
    }
}