using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Telegram.BOT.Infrastructure.Database.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalOrder { get; set; }
    }
}