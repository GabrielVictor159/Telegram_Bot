using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Domain.Enums;
using Telegram.BOT.Domain.Logs;

namespace Telegram.BOT.Services.Jobs.ManagementChats
{
    public class RemoveChatsAfterDayUseCase : BackgroundService
    {
        private readonly ILogRepository logRepository;
        private readonly IChatRepository chatRepository;

        public RemoveChatsAfterDayUseCase
            (ILogRepository logRepository,
            IChatRepository chatRepository)
        {
            this.logRepository = logRepository;
            this.chatRepository = chatRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var logs = new List<Log>();
            try
            {
                var entities = chatRepository.GetByFilter(e => (e.CreateDateTime - DateTime.Now).Days > 1);
                chatRepository.RemoveRange(entities);
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
