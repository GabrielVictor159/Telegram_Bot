using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Chat;

namespace Telegram.BOT.Application.Interfaces.Repositories
{
    public interface IChatRepository
    {
        Chat Add(Chat chat);
        List<Chat> AddRange (List<Chat> chats);
        Chat GetOne(Guid id);
        List<Chat> GetByFilter(Expression<Func<Chat, bool>> expression);
        int Remove(Chat chat);
        int RemoveRange(List<Chat> chats);
    }
}