﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Infrastructure.Database.Entities.Products;
using Telegram.BOT.Infrastructure.Database.Map.Products;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Telegram.BOT.Infrastructure.Database.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context context;
        private readonly IMapper mapper;
        public ProductRepository(Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public Domain.Products.Product Add(Domain.Products.Product product)
        {
            var entity = mapper.Map<Product>(product);
            context.Products.Add(entity);
            context.SaveChanges();
            return mapper.Map<Domain.Products.Product>(entity);
        }
        public List<Domain.Products.Product> GetByFilter(Expression<Func<Domain.Products.Product, bool>> expression, int page, int pageSize)
        {
            var predicate = mapper.Map<Expression<Func<Product, bool>>>(expression);
            var query = context.Products
                .Include(p => p.Groups)
                .Where(predicate);
            var totalItems =  query.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var entities = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return mapper.Map<List<Domain.Products.Product>>(entities);
        }
        public int Update(Domain.Products.Product product)
        {
           var existingProduct = context.Products.Find(product.Id);

            if (existingProduct != null)
            {
                context.Entry(existingProduct).CurrentValues.SetValues(product);
                return context.SaveChanges();
            }
            return 0; 
        }
        public bool Delete(Guid id)
        {
            var product = context.Products.Find(id);
            if(product == null) 
            {
                return false;
            }
            var ProductGroups = context.productGroups.Where(pg=>pg.ProductId==id);
            context.productGroups.RemoveRange(ProductGroups);
            context.Products.Remove(product);
            context.SaveChanges();
            return true;
        }
    }
}
