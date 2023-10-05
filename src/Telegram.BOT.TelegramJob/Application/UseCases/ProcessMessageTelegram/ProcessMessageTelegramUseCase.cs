using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Domain.Enums;
using Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram.Handlers;

namespace Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram
{
    public class ProcessMessageTelegramUseCase : IProcessMessageTelegramRequest
    {
        private readonly ProcessInitialMessageHandler processInitialMessage;
        private readonly ILogRepository logRepository;

        public ProcessMessageTelegramUseCase
            (ProcessInitialMessageHandler processInitialMessage, 
            ILogRepository logRepository)
        {
            this.processInitialMessage = processInitialMessage;
            this.logRepository = logRepository;
        }

        public async Task Execute(ProcessMessageTelegramRequest request)
        {
           try
            {
                await processInitialMessage.ProcessRequest(request);
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
