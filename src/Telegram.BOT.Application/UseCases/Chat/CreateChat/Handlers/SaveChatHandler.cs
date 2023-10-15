using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;

namespace Telegram.BOT.Application.UseCases.Chat.CreateChat.Handlers
{
    public class SaveChatHandler : Handler<CreateChatRequest>
    {
        private readonly IChatRepository chatRepository;
        public SaveChatHandler(IChatRepository chatRepository)
        {
            this.chatRepository = chatRepository;
        }
        public override async Task ProcessRequest(CreateChatRequest request)
        {
            chatRepository.Add(request.newChat);
            request.output = "Chat created successfully";
            if(sucessor !=null)
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}
