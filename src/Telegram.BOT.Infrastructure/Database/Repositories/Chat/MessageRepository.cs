using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Telegram.BOT.Application.Interfaces.Repositories;

namespace Telegram.BOT.Infrastructure.Database.Repositories.Chat;
    public class MessageRepository : IMessageRepository
    {
       private readonly IMapper mapper;
        private readonly Context context;
        public MessageRepository(IMapper mapper, Context context)
        {
            this.mapper=mapper;
            this.context=context;
        }
        public Domain.Chat.Message Add(Domain.Chat.Message message)
        {
            var entity = mapper.Map<Entities.Chat.Message>(message);
            context.messages.Add(entity);
            context.SaveChanges();
            return mapper.Map<Domain.Chat.Message>(entity);
        }

        public List<Domain.Chat.Message> AddRange(List<Domain.Chat.Message> messages)
        {
            var entities = mapper.Map<List<Entities.Chat.Message>>(messages);
            context.messages.AddRange(entities);
            context.SaveChanges();
            return mapper.Map<List<Domain.Chat.Message>>(entities);
        }

        public List<Domain.Chat.Message> GetByFilter(Expression<Func<Domain.Chat.Message, bool>> expression)
        {
            var predicate = mapper.Map<Expression<Func<Entities.Chat.Message, bool>>>(expression);
            return mapper.Map<List<Domain.Chat.Message>>(context.messages.Where(predicate).ToList());
        }

        public Domain.Chat.Message GetOne(Guid id)
        {
            var entity = context.messages.Find(id);
            return mapper.Map<Domain.Chat.Message>(entity);
        }

        public int Remove(Domain.Chat.Message message)
        {
           var entity = context.messages.Find(message.Id);
           if(entity==null)
           {return 0;}
           context.messages.Remove(entity);
           return context.SaveChanges();
        }

        public int RemoveRange(List<Domain.Chat.Message> messages)
        {
           List<Guid> ids = messages.Select(e=>e.Id).ToList();
           var entities = context.messages.Where(e => ids.Contains(e.Id)); 
           context.messages.RemoveRange(entities);
           return context.SaveChanges();
        }
    }
