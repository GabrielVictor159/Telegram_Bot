using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Backgrund.ManagementGroups.Handlers;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Domain.Enums;
using Telegram.BOT.Domain.Logs;

namespace Telegram.BOT.Application.Backgrund.ManagementGroups;

public class RemoveGroupsNotUseCase : BackgroundService
{
    private readonly ILogRepository logRepository;
    private readonly FindGroupsNotUseHandler findGroupsNotUseHandler;
    private readonly RemoveGroupsHandler removeGroupsHandler;

    public RemoveGroupsNotUseCase
        (ILogRepository logRepository, 
        FindGroupsNotUseHandler findGroupsNotUseHandler, 
        RemoveGroupsHandler removeGroupsHandler)
    {
        this.logRepository = logRepository;
        this.findGroupsNotUseHandler = findGroupsNotUseHandler;
        this.removeGroupsHandler = removeGroupsHandler;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var logs = new List<Log>();
        try
        {
            var list = findGroupsNotUseHandler.Process();
            (bool sucessRequest, string messageRequest) = removeGroupsHandler.Process(list);
            if (!sucessRequest)
            {
                logs.Add(
                        Log.AddLog
                        (LogType.Error,
                        messageRequest,
                        "RemoveGroupsNotUseCase"));
            }
        }
        catch(Exception ex) 
        {
            logs.Add(
                        Log.AddLog
                        (LogType.Error,
                        $"Occurring an error: {ex.Message ?? ex.InnerException?.Message}, stacktrace: {ex.StackTrace}",
                        "RemoveGroupsNotUseCase"));
        }
        finally
        {
            logRepository.AddRange(logs);
        }
        await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
    }
}
