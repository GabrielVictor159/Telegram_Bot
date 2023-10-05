using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Enums;
using Telegram.BOT.Domain.Logs;

namespace Telegram.BOT.Application.UseCases.Ambient.EnvVariables.RemoveEnv
{
    public class RemoveEnvRequest : Output<string>
    {
        public required string Key { get; init; }
        public List<Log> Logs { get; set; } = new();
        internal void AddLog(LogType type, string message)
               => Logs.Add(Log.AddLog(type, message, "Remove Env"));
    }
}
