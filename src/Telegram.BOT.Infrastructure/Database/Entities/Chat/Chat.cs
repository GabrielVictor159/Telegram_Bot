using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Telegram.BOT.Infrastructure.Database.Entities.Chat
{
    public class Chat
    {
        public Guid Id { get; set; }
        public DateTime CreateDateTime {get;set;}
    }
}