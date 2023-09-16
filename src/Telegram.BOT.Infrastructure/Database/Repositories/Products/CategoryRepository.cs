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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Context context;
        private readonly IMapper mapper;
        private readonly IMarcRepository marcRepository;
        public CategoryRepository(Context context, IMapper mapper, IMarcRepository marcRepository)
        {
            this.context = context;
            this.mapper = mapper;
            this.marcRepository = marcRepository;
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
                .Include(p=>p.marcs)
                .ToList();
            return mapper.Map<List<Domain.Products.Category>>(entities);
        }
        public int Update(Domain.Products.Category category)
        {
            var existingProduct = context.Categories.Find(category.Id);
            if (existingProduct != null)
            {
                context.Entry(existingProduct).CurrentValues.SetValues(category);
                return context.SaveChanges();
            }
            return 0; 
        }
        public bool Delete(Guid id)
        {
            var category = context.Categories.Find(id);
            if(category == null) 
            {
                return false;
            }
            var marcs = marcRepository.GetByFilter(e=>e.CategoryId==id);
            marcs.ForEach(e=>marcRepository.Delete(e.Id));
            context.Categories.Remove(category);
            context.SaveChanges();
            return true;
        }
    }
}