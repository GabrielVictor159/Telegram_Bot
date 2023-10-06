using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Domain.Enums;
using Telegram.BOT.Domain.Logs;
using Telegram.BOT.Services.Jobs.ManagementImages.Handlers;

namespace Telegram.BOT.Services.Jobs.ManagementImages;

public class RemoveImagesNotUseCase : BackgroundService
{
    private readonly ILogRepository logRepository;
    private readonly RemoveImageHandler removeImageHandler;
    private readonly FindImagesNotUseHandler findImagesNotUseHandler;

    public RemoveImagesNotUseCase
        (ILogRepository logRepository,
        RemoveImageHandler removeImageHandler,
        FindImagesNotUseHandler findImagesNotUseHandler)
    {
        this.logRepository = logRepository;
        this.removeImageHandler = removeImageHandler;
        this.findImagesNotUseHandler = findImagesNotUseHandler;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var logs = new List<Log>();
        try
        {
            var list = findImagesNotUseHandler.Process();
            list.ForEach(l =>
            {
                (bool sucess, string message) = removeImageHandler.Process(l);
                if (sucess)
                {
                    logs.Add(
                        Log.AddLog
                        (LogType.Information,
                        $"Image: ${Environment.GetEnvironmentVariable("ImagesPathByServiceInfra")}/${l} Remove",
                        "RemoveImagesNotUseCase"));
                }
                else
                {
                    logs.Add(
                        Log.AddLog
                        (LogType.Error,
                        message,
                        "RemoveImagesNotUseCase"));
                }
            });
        }
        catch (Exception ex)
        {
            logs.Add(
                        Log.AddLog
                        (LogType.Error,
                        $"Occurring an error: {ex.Message ?? ex.InnerException?.Message}, stacktrace: {ex.StackTrace}",
                        "RemoveImagesNotUseCase"));
        }
        finally
        {
            logRepository.AddRange(logs);
        }
        await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
    }
}
