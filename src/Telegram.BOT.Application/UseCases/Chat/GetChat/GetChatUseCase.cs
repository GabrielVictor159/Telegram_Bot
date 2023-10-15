using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Chat.CreateChat.Handlers;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Chat.GetChat
{
    public class GetChatUseCase : IGetChatRequest
    {
        private readonly IChatRepository chatRepository;
        private readonly ILogRepository logRepository;

        public GetChatUseCase
            (IChatRepository chatRepository,
            ILogRepository logRepository)
        {
            this.chatRepository = chatRepository;
            this.logRepository = logRepository;
        }

        public void Execute(GetChatRequest request)
        {
            try
            {
                request.AddLog(LogType.Process, "GetChatUseCase");
                var entities = chatRepository.GetByFilter(request.expression);
                request.output = new Boundaries.Chat.GetChatOutput() { Chats = entities };
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
