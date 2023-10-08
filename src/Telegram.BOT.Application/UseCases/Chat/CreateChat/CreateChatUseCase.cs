using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Boundaries.Products;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Chat.CreateChat.Handlers;
using Telegram.BOT.Application.UseCases.Products.CreateProduct.Handlers;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Chat.CreateChat
{
    public class CreateChatUseCase : ICreateChatRequest
    {
        private readonly ILogRepository logRepository;
        private readonly ValidateChatHandler validateChatHandler;

        public CreateChatUseCase
            (ILogRepository logRepository, 
            ValidateChatHandler validateChatHandler,
            SaveChatHandler saveChatHandler)
        {
            validateChatHandler.SetSucessor(saveChatHandler);
            this.logRepository = logRepository;
            this.validateChatHandler = validateChatHandler;
        }

        public async Task Execute(CreateChatRequest request)
        {
            try
            {
                await validateChatHandler.ProcessRequest(request);
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
