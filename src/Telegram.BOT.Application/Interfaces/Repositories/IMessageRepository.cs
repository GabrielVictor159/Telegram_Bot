using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Chat;

namespace Telegram.BOT.Application.Interfaces.Repositories
{
    public interface IMessageRepository
    {
        Message Add(Message message);
        List<Message> AddRange (List<Message> messages);
        Message GetOne(Guid id);
        List<Message> GetByFilter(Expression<Func<Message, bool>> expression);
        int Remove(Message message);
        int RemoveRange(List<Message> messages);
    }
}