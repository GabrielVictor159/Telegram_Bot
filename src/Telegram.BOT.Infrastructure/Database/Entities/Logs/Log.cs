using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Infrastructure.Database.Entities.Logs
{
    public class Log
    {
        public Guid Id { get; set; }
        public required LogType Type { get; set; }
        public required string UseCase { get; set; }
        public required string Message { get; set; }
        public required DateTime LogDate { get; set; }
    }
}
