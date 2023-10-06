using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Telegram.BOT.Infrastructure.Database.Entities.Chat
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Messaging {get; set;} = "";
        public int NumberMessage { get; init; }
        public Guid ChatId {get;set;}
        public virtual Chat? Chat {get;set;}
    }
}