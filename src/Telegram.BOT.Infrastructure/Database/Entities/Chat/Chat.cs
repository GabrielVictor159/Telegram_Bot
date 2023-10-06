using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Telegram.BOT.Infrastructure.Database.Entities.Chat
{
    public class Chat
    {
        public Guid Id { get; set; }
        public required string IdTelegram { get; init; }
        public string Username { get; init; } = "";
        public string Location { get; init; } = "";
        public string Description { get; init; } = "";
        public DateTime CreateDateTime {get;set;}
    }
}