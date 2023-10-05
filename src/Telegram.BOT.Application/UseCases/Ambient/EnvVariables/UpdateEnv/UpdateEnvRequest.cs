using ManagementServices.variables.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Enums;
using Telegram.BOT.Domain.Logs;

namespace Telegram.BOT.Application.UseCases.Ambient.EnvVariables.UpdateEnv
{
    public class UpdateEnvRequest : Output<string>
    {
        public required EnvVariable variable { get; init; }  
        public List<Log> Logs { get; set; } = new();
        internal void AddLog(LogType type, string message)
               => Logs.Add(Log.AddLog(type, message, "Update Env"));
    }
}
