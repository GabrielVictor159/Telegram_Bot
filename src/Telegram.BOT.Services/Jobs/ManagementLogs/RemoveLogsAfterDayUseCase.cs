using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Domain.Enums;
using Telegram.BOT.Domain.Logs;

namespace Telegram.BOT.Services.Jobs.ManagementLogs
{
    public class RemoveLogsAfterDayUseCase : BackgroundService
    {
        private readonly ILogRepository logRepository;

        public RemoveLogsAfterDayUseCase
            (ILogRepository logRepository)
        {
            this.logRepository = logRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var logs = new List<Log>();
            try
            {
                var entities = logRepository.GetByFilter(e => (DateTime.Now - e.LogDate).Days > 3).ToList();
                logRepository.RemoveRange(entities);
            }
            catch (Exception ex)
            {
                logs.Add(
                            Log.AddLog
                            (LogType.Error,
                            $"Occurring an error: {ex.Message ?? ex.InnerException?.Message}, stacktrace: {ex.StackTrace}",
                            "RemoveLogsAfterDayUseCase"));
            }
            finally
            {
                logRepository.AddRange(logs);
            }
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }
    }
}
