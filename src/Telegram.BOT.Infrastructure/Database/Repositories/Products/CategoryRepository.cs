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
        public List<Domain.Products.Category> GetByFilter(Expression<Func<Domain.Products.Category, bool>> expression, int page, int pageSize)
        {
            var predicate = mapper.Map<Expression<Func<Category, bool>>>(expression);
            var query = context.Categories
                .Include(p => p.marcs)
                .Where(predicate);
            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var entities = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
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
            var marcs = marcRepository.GetByFilter(e=>e.CategoryId==id,1,int.MaxValue);
            marcs.ForEach(e=>marcRepository.Delete(e.Id));
            context.Categories.Remove(category);
            context.SaveChanges();
            return true;
        }
    }
}