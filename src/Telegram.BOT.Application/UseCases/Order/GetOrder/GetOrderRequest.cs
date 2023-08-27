using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Telegram.BOT.Application.UseCases.Order.GetOrder
{

    public class GetOrderRequest
    {
        public Expression<Func<Domain.Order.Order, bool>> func { get; private set; }
        public List<Domain.Order.Order>? orderResult { get; private set; }
        public GetOrderRequest(Expression<Func<Domain.Order.Order, bool>> func)
        {
            this.func = func;
        }
        public void SetOutput(List<Domain.Order.Order> orderResult)
         => this.orderResult = orderResult;
    }
}