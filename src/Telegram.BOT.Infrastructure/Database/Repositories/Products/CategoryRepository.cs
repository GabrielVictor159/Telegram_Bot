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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Context context;
        private readonly IMapper mapper;
        public CategoryRepository(Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
         public Domain.Products.Category Add(Domain.Products.Category category)
        {
            var entity = mapper.Map<Category>(category);
            context.Categories.Add(entity);
            context.SaveChanges();
            return mapper.Map<Domain.Products.Category>(entity);
        }
        public List<Domain.Products.Category> GetByFilter(Expression<Func<Domain.Products.Category, bool>> expression)
        {
            var predicate = mapper.Map<Expression<Func<Category, bool>>>(expression);
            var entities = context.Categories
                .Where(predicate)
                .ToList();
            return mapper.Map<List<Domain.Products.Category>>(entities);
        }
        public int Update(Domain.Products.Category category)
        {
            var ProductGroups = context.ProductGroups
                .Where(pg => pg.Product25Id == category.Id || pg.Product50Id == category.Id || pg.Product75Id == category.Id);
            context.ProductGroups.RemoveRange(ProductGroups);
            context.Categories.Update(mapper.Map<Category>(category));
            return context.SaveChanges();
        }
        public bool Delete(Guid id)
        {
            var category = context.Products.Find(id);
            if(category == null) 
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