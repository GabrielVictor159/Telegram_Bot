using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Telegram.BOT.Application.UseCases.Order.CreateOrder
{
    public interface ICreateOrderRequest
    {
        Task Execute(CreateOrderRequest request);
    }
}