using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Telegram.BOT.Application.Interfaces.Repositories;

namespace Telegram.BOT.Infrastructure.Database.Repositories.Products
{
    public class ProductGroupsRepository : IProductGroupsRepository
    {
        private readonly IMapper mapper;
        private readonly Context context;
        public ProductGroupsRepository(IMapper mapper, Context context)
        {
            this.mapper=mapper;
            this.context=context;
        }
        public Domain.Products.ProductGroups Add(Domain.Products.ProductGroups chat)
        {
            var entity = mapper.Map<Entities.Products.ProductGroups>(chat);
            context.productGroups.Add(entity);
            context.SaveChanges();
            return mapper.Map<Domain.Products.ProductGroups>(entity);
        }

        public List<Domain.Products.ProductGroups> AddRange(List<Domain.Products.ProductGroups> productGroups)
        {
            var entities = mapper.Map<List<Entities.Products.ProductGroups>>(productGroups);
            context.productGroups.AddRange(entities);
            context.SaveChanges();
            return mapper.Map<List<Domain.Products.ProductGroups>>(entities);
        }

        public List<Domain.Products.ProductGroups> GetByFilter(Expression<Func<Domain.Products.ProductGroups, bool>> expression)
        {
            var predicate = mapper.Map<Expression<Func<Entities.Products.ProductGroups, bool>>>(expression);
            return mapper.Map<List<Domain.Products.ProductGroups>>
            (
                context.productGroups
                .Where(predicate)
                .Include(p=>p.Group)
                .Include(p=>p.Product25)
                .Include(p=>p.Product50)
                .Include(p=>p.Product75)
                .ToList()
            );
        }

        public Domain.Products.ProductGroups GetOne(Guid id)
        {
            var entity = context.productGroups.
            Where(e=>e.Id==id)
            .Include(p=>p.Group)
            .Include(p=>p.Product25)
            .Include(p=>p.Product50)
            .Include(p=>p.Product75)
            .First();
            return mapper.Map<Domain.Products.ProductGroups>(entity);
        }

        public int Remove(Domain.Products.ProductGroups chat)
        {
           var entity = context.productGroups.Find(chat.Id);
           if(entity==null)
           {return 0;}
           context.productGroups.Remove(entity);
           return context.SaveChanges();
        }

        public int RemoveRange(List<Domain.Products.ProductGroups> productGroups)
        {
           List<Guid> ids = productGroups.Select(e=>e.Id).ToList();
           var entities = context.productGroups.Where(e => ids.Contains(e.Id)); 
           context.productGroups.RemoveRange(entities);
           return context.SaveChanges();
        }
    }
}