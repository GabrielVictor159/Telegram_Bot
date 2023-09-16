using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Infrastructure.Database.Entities.Products;

namespace Telegram.BOT.Infrastructure.Database.Repositories.Products
{
    public class MarcRepository : IMarcRepository
    {
        private readonly Context context;
        private readonly IMapper mapper;
        private IProductRepository productRepository;
        public MarcRepository(Context context, IMapper mapper, IProductRepository productRepository)
        {
            this.context = context;
            this.mapper = mapper;
            this.productRepository = productRepository;
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
                .Include(p=>p.Category)
                .Include(p=>p.products)
                .ToList();
            return mapper.Map<List<Domain.Products.Marc>>(entities);
        }
        public int Update(Domain.Products.Marc marc)
        {
             var existingProduct = context.Marcs.Find(marc.Id);
            if (existingProduct != null)
            {
                context.Entry(existingProduct).CurrentValues.SetValues(marc);
                return context.SaveChanges();
            }
            return 0; 
        }
        public bool Delete(Guid id)
        {
            var entity = context.Marcs.Find(id);
            if(entity==null)
            {
                return false;
            }
            var products = productRepository.GetByFilter(e=>e.MarcId==id);
            products.ForEach(e=>productRepository.Delete(e.Id));
            context.Marcs.Remove(entity);
            context.SaveChanges();
            return true;
        }
    }
}