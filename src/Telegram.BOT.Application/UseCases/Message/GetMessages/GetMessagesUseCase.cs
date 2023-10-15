using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Boundaries.Marc;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Marc.GetMarc.Handlers;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Message.GetMessages
{
    public class GetMessagesUseCase : IGetMessagesRequest
    {
        private readonly ILogRepository logRepository;
        private readonly IMessageRepository messageRepository;

        public GetMessagesUseCase(ILogRepository logRepository, IMessageRepository messageRepository)
        {
            this.logRepository = logRepository;
            this.messageRepository = messageRepository;
        }

        public void Execute(GetMessagesRequest request)
        {
            try
            {
                request.AddLog(LogType.Process, "Search messages");
                var entities = messageRepository.GetByFilter(request.Expression);
                request.output = new() { Messages = entities };
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
