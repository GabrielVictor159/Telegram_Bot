using ManagementServices.variables.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Enums;
using Telegram.BOT.Domain.Logs;

namespace Telegram.BOT.Application.UseCases.Ambient.EnvVariables.CreateEnv;

public class CreateEnvRequest : Output<string>
{
    public required EnvVariable EnvVariable { get; init; }
    public List<Log> Logs { get; set; } = new();
    public void AddLog(LogType type, string message)
           => Logs.Add(Log.AddLog(type, message, "Create Env"));
}
