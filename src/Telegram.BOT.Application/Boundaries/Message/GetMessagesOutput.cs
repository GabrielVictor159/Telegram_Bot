using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.BOT.Application.Boundaries.Message
{
    public class GetMessagesOutput
    {
        public required List<Domain.Chat.Message> Messages { get; init; }
    }
}
