using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Boundaries.Chat;
using Telegram.BOT.Domain.Enums;
using Telegram.BOT.Domain.Logs;

namespace Telegram.BOT.Application.UseCases.Chat.GetChat
{
    public class GetChatRequest : Output<GetChatOutput>
    {
        public required Expression<Func<Domain.Chat.Chat, bool>> expression { get; init; }
        public List<Log> Logs { get; set; } = new();
        public void AddLog(LogType type, string message)
               => Logs.Add(Log.AddLog(type, message, "Get Chat"));
    }
}
