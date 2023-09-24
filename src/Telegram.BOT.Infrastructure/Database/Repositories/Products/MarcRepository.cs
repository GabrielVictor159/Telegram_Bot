using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Infrastructure.Database.Entities.Products;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public List<Domain.Products.Marc> GetByFilter(Expression<Func<Domain.Products.Marc, bool>> expression, int page, int pageSize)
        {
            var predicate = mapper.Map<Expression<Func<Marc, bool>>>(expression);
            var query = context.Marcs
                .Include(p => p.Category)
                .Include(p => p.products)
                .Where(predicate);
            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var entities = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
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
            var products = productRepository.GetByFilter(e=>e.MarcId==id,1,int.MaxValue);
            products.ForEach(e=>productRepository.Delete(e.Id));
            context.Marcs.Remove(entity);
            context.SaveChanges();
            return true;
        }
    }
}