using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Domain.Logs
{
    public class Log
    {
        public required Guid Id { get; init; }
        public required LogType Type { get; init; }
        public required string UseCase { get; init; }
        public required string Message { get; init; }
        public required DateTime LogDate { get; init; }
        public static Log AddLog(LogType type, string message, string useCase)
        => new Log() { Id = Guid.NewGuid(), Type = type, LogDate = DateTime.Now, Message = message, UseCase = useCase};

    }
}
