using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Validator.Chat;

namespace Telegram.BOT.Domain.Chat
{
    public class Chat : Entity
    {
        public Guid Id { get;private set; }
        public DateTime CreateDateTime {get;private set;}
        public Chat(Guid Id, DateTime CreateDateTime)
        {
            this.Id = Id;
            this.CreateDateTime = CreateDateTime;
            Validate(this, new ChatValidator());
        }
    }
}