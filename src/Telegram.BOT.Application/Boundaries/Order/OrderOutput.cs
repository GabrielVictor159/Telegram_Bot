using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Telegram.BOT.Application.Boundaries.Order
{
    public class OrderOutput
    {
        public Guid id { get; private set; }
        public OrderOutput(Guid id)
        {
            this.id = id;
        }
    }
}