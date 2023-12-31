﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Domain.Enums;
using Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram.Handlers;
using Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram.Handlers.MenuFlux;

namespace Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram
{
    public class ProcessMessageTelegramUseCase : IProcessMessageTelegramRequest
    {
        private readonly InitialSearchChatHandler initialSearchChatHandler;
        private readonly ILogRepository logRepository;
        private readonly INotificationService notificationService;

        public ProcessMessageTelegramUseCase
            (InitialSearchChatHandler initialSearchChatHandler, 
            ILogRepository logRepository,
            INotificationService notificationService)
        {
            this.initialSearchChatHandler = initialSearchChatHandler;
            this.logRepository = logRepository;
            this.notificationService = notificationService;
        }

        public async Task Execute(ProcessMessageTelegramRequest request)
        {
           try
            {
                await initialSearchChatHandler.ProcessRequest(request);
                if (notificationService.HasNotifications)
                {
                    notificationService.Notifications.ForEach(e => { request.AddLog(LogType.Process, e.Message); });
                }
                Console.WriteLine("ProcessMessageTelegramUseCase");
            }
            catch (Exception ex)
            {
                request.AddLog(LogType.Error, $"Occurring an error: {ex.Message ?? ex.InnerException?.Message}, stacktrace: {ex.StackTrace}");
            }
            finally
            {
                logRepository.AddRange(request.Logs);
            }
            
        }
    }
}
