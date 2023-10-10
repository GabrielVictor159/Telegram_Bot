using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;

namespace Telegram.BOT.Application.UseCases.Chat.CreateChat.Handlers
{
    public class ValidateChatHandler : Handler<CreateChatRequest>
    {
        private readonly IChatRepository chatRepository;
        public ValidateChatHandler(IChatRepository chatRepository)
        {
            this.chatRepository = chatRepository;
        }
        public override async Task ProcessRequest(CreateChatRequest request)
        {
            if(!request.newChat.IsValid)
            {
                request.ErrorMessage = request.newChat.ValidationResult!.ToString();
                request.IsError = true;
                return;
            }
            if(chatRepository.GetByFilter(e=>e.IdTelegram.Equals(request.newChat.IdTelegram)).Any())
            {
                request.ErrorMessage = "There is already a chat with the same IdTelegram";
                request.IsError = true;
                return;
            }
            if(sucessor!=null)
            {
                await sucessor.ProcessRequest(request);
            }

        }
    }
}
