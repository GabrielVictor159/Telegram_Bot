using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Infrastructure.Database.Entities.Products;
using Telegram.BOT.Infrastructure.Service;

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
             var entity = context.Groups.Where(e=>e.Id==id)
             .Include(p=>p.Group)
             .First();
            return mapper.Map<Domain.Products.Groups>(entity);
        }
        public List<Domain.Products.Groups> GetByFilter(Expression<Func<Domain.Products.Groups, bool>> expression)
        {
            var predicate = mapper.Map<Expression<Func<Groups, bool>>>(expression);
            return mapper.Map<List<Domain.Products.Groups>>(context.Groups.Where(predicate).Include(p=>p.Group).ToList());
        }
        public List<Domain.Products.Groups> GetByLeveinsthein(string s1, double percentagem)
        {
            return mapper.Map<List<Domain.Products.Groups>>
            (
                context.Groups
                .Where(e => ProbabilityOperations.CalculateNormalizedLevenshteinDistance(e.Tags,s1)>=percentagem)
                .Include(p => p.Group)
                .ToList()
            );
        }
        public bool Remove(Guid id)
        {
            var entity = context.Groups.Find(id);
            if(entity == null)
            {
                return false;
            }
            var productGroup = context.productGroups.Where(pg => pg.GroupId == id).ToList();
            context.productGroups.RemoveRange(productGroup);
            context.Groups.Remove(entity);
            return context.SaveChanges() > 0;
        }

    }
}
