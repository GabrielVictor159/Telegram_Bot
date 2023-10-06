using ManagementServices.variables.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Boundaries.Ambient.EnvVariables;
using Telegram.BOT.Domain.Enums;
using Telegram.BOT.Domain.Logs;

namespace Telegram.BOT.Application.UseCases.Ambient.EnvVariables.GetEnv;

public class GetEnvRequest : Output<GetEnvOutput> 
{
    public required string Key { get; init; }
    public EnvVariable? EnvVariable { get; set; }
    public List<Log> Logs { get; set; } = new();
    internal void AddLog(LogType type, string message)
           => Logs.Add(Log.AddLog(type, message, "Get Env"));
}
