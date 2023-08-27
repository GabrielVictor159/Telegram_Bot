using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Telegram.BOT.Application.UseCases.Order.GetOrder
{
    public interface IGetOrderRequest
    {
        Task Execute(GetOrderRequest request);
    }
}