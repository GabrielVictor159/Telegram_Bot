﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Infrastructure.Database.Entities.Products;

namespace Telegram.BOT.Infrastructure.Database.Repositories.Products
{
    public class GroupsRepository : IGroupsRepository
    {
        private readonly Context context;
        private readonly IMapper mapper;
        public GroupsRepository(Context context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public Domain.Products.Groups Add(Domain.Products.Groups group)
        {
            var entity = mapper.Map<Groups>(group);
            context.Groups.Add(entity);
            context.SaveChanges();
            return mapper.Map<Domain.Products.Groups>(entity);
        }
        public List<Domain.Products.Groups> AddRange(List<Domain.Products.Groups> groups) 
        {
            var entities = mapper.Map<List<Groups>>(groups);
            context.Groups.AddRange(entities);
            context.SaveChanges();
            return mapper.Map<List<Domain.Products.Groups>>(entities);
        }
        public Domain.Products.Groups? GetOne(Guid id)
        {
            var entity = context.Groups.Find(id);
            if(entity != null) 
            {
                return mapper.Map<Domain.Products.Groups>(entity);
            }
            return null;
        }
        public List<Domain.Products.Groups> GetByFilter(Expression<Func<Domain.Products.Groups, bool>> expression)
        {
            var predicate = mapper.Map<Expression<Func<Groups, bool>>>(expression);
            return mapper.Map<List<Domain.Products.Groups>>(context.Groups.Where(predicate).ToList());
        }
        public bool Remove(Guid id)
        {
            var entity = context.Groups.Find(id);
            if(entity == null)
            {
                return false;
            }
            var productGroup = context.ProductGroups.Where(pg => pg.GroupId == id).ToList();
            context.ProductGroups.RemoveRange(productGroup);
            context.Groups.Remove(entity);
            return context.SaveChanges() > 0;
        }

    }
}
