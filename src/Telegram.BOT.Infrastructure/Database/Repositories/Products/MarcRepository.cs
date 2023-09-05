using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Infrastructure.Database.Entities.Products;

namespace Telegram.BOT.Infrastructure.Database.Repositories.Products
{
    public class MarcRepository : IMarcRepository
    {
        private readonly Context context;
        private readonly IMapper mapper;
        public MarcRepository(Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
         public Domain.Products.Marc Add(Domain.Products.Marc marc)
        {
            var entity = mapper.Map<Marc>(marc);
            context.Marcs.Add(entity);
            context.SaveChanges();
            return mapper.Map<Domain.Products.Marc>(entity);
        }
        public List<Domain.Products.Marc> GetByFilter(Expression<Func<Domain.Products.Marc, bool>> expression)
        {
            var predicate = mapper.Map<Expression<Func<Marc, bool>>>(expression);
            var entities = context.Marcs
                .Where(predicate)
                .ToList();
            return mapper.Map<List<Domain.Products.Marc>>(entities);
        }
        public int Update(Domain.Products.Marc marc)
        {
            var ProductGroups = context.ProductGroups
                .Where(pg => pg.Product25Id == marc.Id || pg.Product50Id == marc.Id || pg.Product75Id == marc.Id);
            context.ProductGroups.RemoveRange(ProductGroups);
            context.Marcs.Update(mapper.Map<Marc>(marc));
            return context.SaveChanges();
        }
        public bool Delete(Guid id)
        {
            var marc = context.Products.Find(id);
            if(marc == null) 
            {
                return false;
            }
            var ProductGroups = context.ProductGroups.Where(pg=>pg.Product25Id==id||pg.Product50Id==id||pg.Product75Id==id);
            context.ProductGroups.RemoveRange(ProductGroups);
            context.SaveChanges();
            return true;
        }
    }
}