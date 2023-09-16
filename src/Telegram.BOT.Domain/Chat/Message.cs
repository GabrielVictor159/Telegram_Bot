using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Validator.Chat;

namespace Telegram.BOT.Domain.Chat
{
    public class Message : Entity
    {
        public Guid Id { get; private set; }
        public string Messaging {get; private set;} = "";
        public Guid ChatId {get; private set;}
        public Chat? Chat {get;  set;}

        public Message(Guid Id, string Messaging, Guid ChatId)
        {
            this.Id = Id;
            this.Messaging = Messaging;
            this.ChatId = ChatId;
            Validate(this, new MessageValidator());
        }
    }
}