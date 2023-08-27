using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Telegram.BOT.WebApi.UseCases.Order.RemoveOrder
{
    public class OrderResponse
    {
        public string RemoveResult { get; private set; } = "";
        public OrderResponse(string RemoveResult)
        {
            this.RemoveResult = RemoveResult;
        }
    }
}