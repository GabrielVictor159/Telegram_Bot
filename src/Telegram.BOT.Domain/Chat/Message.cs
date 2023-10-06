using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Validator.Chat;

namespace Telegram.BOT.Domain.Chat;

public class Message : Entity<Message,MessageValidator>
{
    public required Guid Id { get; init; }
    public required string Messaging {get; init;} = "";
    public required Guid ChatId {get; init;}
    public required int NumberMessage { get; init;}
    public Chat? Chat {get;  set;}

    public Message()
        : base(new MessageValidator())
    {
    }
}