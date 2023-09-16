using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Infrastructure.Database.Entities.Chat;

namespace Telegram.BOT.Infrastructure.Database.Repositories.Chat
{
    public class ChatRepository : IChatRepository
    {
        private readonly IMapper mapper;
        private readonly Context context;
        public ChatRepository(IMapper mapper, Context context)
        {
            this.mapper=mapper;
            this.context=context;
        }
        public Domain.Chat.Chat Add(Domain.Chat.Chat chat)
        {
            var entity = mapper.Map<Entities.Chat.Chat>(chat);
            context.chats.Add(entity);
            context.SaveChanges();
            return mapper.Map<Domain.Chat.Chat>(entity);
        }

        public List<Domain.Chat.Chat> AddRange(List<Domain.Chat.Chat> chats)
        {
            var entities = mapper.Map<List<Entities.Chat.Chat>>(chats);
            context.chats.AddRange(entities);
            context.SaveChanges();
            return mapper.Map<List<Domain.Chat.Chat>>(entities);
        }

        public List<Domain.Chat.Chat> GetByFilter(Expression<Func<Domain.Chat.Chat, bool>> expression)
        {
            var predicate = mapper.Map<Expression<Func<Entities.Chat.Chat, bool>>>(expression);
            return mapper.Map<List<Domain.Chat.Chat>>(context.chats.Where(predicate).ToList());
        }

        public Domain.Chat.Chat GetOne(Guid id)
        {
            var entity = context.chats.Find(id);
            return mapper.Map<Domain.Chat.Chat>(entity);
        }

        public int Remove(Domain.Chat.Chat chat)
        {
           var entity = context.chats.Find(chat.Id);
           if(entity==null)
           {return 0;}
           context.chats.Remove(entity);
           return context.SaveChanges();
        }

        public int RemoveRange(List<Domain.Chat.Chat> chats)
        {
           List<Guid> ids = chats.Select(e=>e.Id).ToList();
           var entities = context.chats.Where(e => ids.Contains(e.Id)); 
           context.chats.RemoveRange(entities);
           return context.SaveChanges();
        }
    }
}