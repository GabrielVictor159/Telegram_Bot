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
        public string IdTelegram { get; private set; } = "";
        public string Username { get; private set; } = "";
        public string Location { get; private set; } = "";
        public string Description { get; private set; } = "";
        public DateTime CreateDateTime {get;private set;}
        public static ChatBuilder New()
        {
            return new ChatBuilder()
            {
                Id = Guid.NewGuid(),
                IdTelegram = "6488494",
                Username = "Username teste",
                Location = "Location Teste",
                Description = "Description test",
                CreateDateTime = DateTime.Now
            };
        }
        public BOT.Domain.Chat.Chat Build()
        {
            return new BOT.Domain.Chat.Chat()
            { 
            Id = Id,
            IdTelegram = IdTelegram,
            Username = Username,
            Location = Location,
            Description = Description,
            CreateDateTime = CreateDateTime,
            };
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
        public ChatBuilder WithDescription(string value) 
        {
            Description=value;
            return this;
        }
        public ChatBuilder WithIdTelegram(string value )
        {
            IdTelegram=value;
            return this;
        }
        public ChatBuilder WithUsername(string value)
        {
            Username = value;
            return this;
        }
        public ChatBuilder WithLocation(string value) 
        {
            Location=value;
            return this;
        }
    }
}