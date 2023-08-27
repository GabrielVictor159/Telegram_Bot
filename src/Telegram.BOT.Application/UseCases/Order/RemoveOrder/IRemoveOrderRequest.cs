using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Telegram.BOT.Application.UseCases.Order.RemoveOrder
{
    public interface IRemoveOrderRequest
    {
        Task Execute(RemoveOrderRequest request);
    }
}