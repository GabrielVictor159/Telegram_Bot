using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Domain.Order;
using Telegram.BOT.Infrastructure.Database.Entities;

namespace Telegram.BOT.Infrastructure.Database.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMapper _mapper;
        private readonly Context _context;
        public OrderRepository(IMapper mapper, Context context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task Add(Domain.Order.Order order)
        {
            Entities.Order newOrder = _mapper.Map<Entities.Order>(order);
            await _context.Orders.AddAsync(newOrder);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> Delete(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order is not null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<List<Domain.Order.Order>> GetOrder(Expression<Func<Domain.Order.Order, bool>> func)
        {
            var predicate = _mapper.Map<Expression<Func<Entities.Order, bool>>>(func);
            var entity = await Task.FromResult(_context.Orders.Where(predicate).ToList());
            return _mapper.Map<List<Domain.Order.Order>>(entity);
        }
        public async Task Update(Domain.Order.Order order)
        {
            var entity = _mapper.Map<Entities.Order>(order);
            _context.Orders.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}