using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Validator.Chat;

namespace Telegram.BOT.Domain.Chat;

public class Chat : Entity<Chat,ChatValidator>
{
    public required Guid Id { get;init; }
    public required string IdTelegram { get;init; }
    public string Username { get; init; } = "";
    public string Location { get; init; } = "";
    public string Description { get; init; } = "";
    public required DateTime CreateDateTime {get;init;}
    public Chat()
        : base( new ChatValidator())
    {  
    }

}