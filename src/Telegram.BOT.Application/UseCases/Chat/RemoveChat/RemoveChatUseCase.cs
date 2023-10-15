using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Chat.RemoveChat
{
    public class RemoveChatUseCase : IRemoveChatRequest
    {
        private readonly ILogRepository logRepository;
        private readonly IChatRepository chatRepository;

        public RemoveChatUseCase(ILogRepository logRepository, IChatRepository chatRepository)
        {
            this.logRepository = logRepository;
            this.chatRepository = chatRepository;
        }

        public void Execute(RemoveChatRequest request)
        {
            try
            {
                request.AddLog(LogType.Process, "RemoveChatUseCase");
                var entity = chatRepository.GetOne(request.Id);
                if(entity == null) 
                {
                    request.IsError = true;
                    request.ErrorMessage = "Chat not found";
                }
                else
                {
                    chatRepository.Remove(entity);
                    request.output = "Chat remove";
                }
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
