using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.BOT.Application.Boundaries.Chat
{
    public class GetChatOutput
    {
        public required List<Domain.Chat.Chat> Chats { get; init; }
    }
}
