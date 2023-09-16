using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Chat;

namespace Telegram.BOT.tests.Builder.Domain.Chat
{
    public class ChatBuilder
    {
        public Guid Id { get;private set; }
        public DateTime CreateDateTime {get;private set;}
        public static ChatBuilder New()
        {
            return new ChatBuilder()
            {
                Id = Guid.NewGuid(),
                CreateDateTime = DateTime.Now
            };
        }
        public BOT.Domain.Chat.Chat Build()
        {
            return new BOT.Domain.Chat.Chat(Id,CreateDateTime);
        }
        public ChatBuilder WithId(Guid value)
        {
            Id=value;
            return this;
        }
        public ChatBuilder WithCreateDateTime(DateTime value)
        {
            CreateDateTime = value;
            return this;
        }
    }
}