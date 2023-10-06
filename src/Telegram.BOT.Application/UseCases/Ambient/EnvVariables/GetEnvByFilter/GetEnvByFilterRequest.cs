using ManagementServices.variables.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Boundaries.Ambient.EnvVariables;
using Telegram.BOT.Domain.Enums;
using Telegram.BOT.Domain.Logs;

namespace Telegram.BOT.Application.UseCases.Ambient.EnvVariables.GetEnvByFilter;

public class GetEnvByFilterRequest : Output<List<GetEnvOutput>>
{
    public string Key { get; init; } = "";
    public string Value { get; init; } = "";
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public List<EnvVariable> variablesFound { get; set; } = new();
    public List<Log> Logs { get; set; } = new();
    internal void AddLog(LogType type, string message)
           => Logs.Add(Log.AddLog(type, message, "Get Env By Filter"));
}
