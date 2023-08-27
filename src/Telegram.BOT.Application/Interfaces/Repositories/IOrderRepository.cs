using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Order;

namespace Telegram.BOT.Application.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task Add(Order order);
        Task Update(Order order);
        Task<bool> Delete(Guid id);
        Task<List<Order>> GetOrder(Expression<Func<Order, bool>> func);
    }
}