using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace Telegram.BOT.tests.Builder.Domain.Chat
{
    public class MessageBuilder
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Messaging {get; private set;} = "Message Test";
        public int numberMessage { get; private set; } = 5;
        public BOT.Domain.Chat.Chat? Chat {get;  set;} = ChatBuilder.New().Build();

        public static MessageBuilder New()
        {
            return new MessageBuilder();
        }
        public BOT.Domain.Chat.Message Build()
        {
            return new BOT.Domain.Chat.Message()
            { 
            Id = Id,
            Messaging = Messaging,
            NumberMessage = numberMessage,
            Chat = Chat,
            ChatId = Chat!.Id,
            };
        }
        public MessageBuilder WithId(Guid value)
        {
            Id=value;
            return this;
        }
        public MessageBuilder WithMessaging(string value)
        {
            Messaging=value;
            return this;
        }
        public MessageBuilder WithChat(BOT.Domain.Chat.Chat value)
        {
            Chat=value;
            return this;
        }
        public MessageBuilder WithNumberMessage(int value) 
        {
            numberMessage = value;
            return this;
        }
    }
}