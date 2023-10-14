using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.UseCases;
using Telegram.BOT.Application.UseCases.Chat.GetChat;
using Telegram.BOT.Application.UseCases.Chat.RemoveChat;

namespace Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram.Handlers.ChatFlux
{
    public class ProcessExitMessageHandler : Handler<ProcessMessageTelegramRequest>
    {
        private readonly IRemoveChatRequest removeChatRequest;
        private readonly IGetChatRequest getChatRequest;

        public ProcessExitMessageHandler
            (IRemoveChatRequest removeChatRequest,
            IGetChatRequest getChatRequest)
        {
            this.removeChatRequest = removeChatRequest;
            this.getChatRequest = getChatRequest;  
        }

        public override async Task ProcessRequest(ProcessMessageTelegramRequest request)
        {
            if(request.text!.ToLower().Equals("/exit"))
            {
                var requestGet = new GetChatRequest() { expression = e => e.IdTelegram == request.id.ToString() };
                getChatRequest.Execute(requestGet);
                if(requestGet.IsError==false && requestGet.output!.Chats.Any())
                {
                    removeChatRequest.Execute(new() { Id = requestGet.output.Chats.First().Id });
                }
                return;
            }
            if(sucessor != null) 
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}
