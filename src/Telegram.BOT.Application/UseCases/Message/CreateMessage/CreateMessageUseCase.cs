using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Boundaries.Marc;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Marc.GetMarc.Handlers;
using Telegram.BOT.Application.UseCases.Message.CreateMessage.Handlers;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Message.CreateMessage
{
    public class CreateMessageUseCase : ICreateMessageRequest
    {
        private readonly ILogRepository logRepository;
        private readonly ValidateDomainHandler validateDomainHandler;

        public CreateMessageUseCase
            (ILogRepository logRepository, 
            ValidateDomainHandler validateDomainHandler,
            VerifyMessageFourteenHandler verifyMessageFourteenHandler,
            SaveMessageHandler saveMessageHandler)
        {
            this.logRepository = logRepository;
            validateDomainHandler.SetSucessor
                (
                verifyMessageFourteenHandler.SetSucessor
                (
                    saveMessageHandler
                )
                );
            this.validateDomainHandler = validateDomainHandler;
        }

        public async Task Execute(CreateMessageRequest request)
        {
            try
            {
                await validateDomainHandler.ProcessRequest(request);
                request.output ="Sucess Persist Message";
            }
            catch (Exception ex)
            {
                request.AddLog(LogType.Error, $"Occurring an error: {ex.Message ?? ex.InnerException?.Message}, stacktrace: {ex.StackTrace}");
                request.IsError = true;
                request.ErrorMessage = ex.Message ?? "";
            }
            finally
            {
                logRepository.AddRange(request.Logs);
            }
        }
    }
}
