using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.UseCases;
using Telegram.BOT.Application.UseCases.Chat.GetChat;
using Telegram.BOT.Application.UseCases.Chat.RemoveChat;
using Telegram.BOT.Application.UseCases.Message.GetMessages;
using Telegram.BOT.Application.UseCases.Products.GetByLeveinstheim;

namespace Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram.Handlers.ChatFlux
{
    public class GetMessagesHandler : Handler<ProcessMessageTelegramRequest>
    {
        private readonly IGetMessagesRequest getMessagesRequest;
        private readonly IGetChatRequest getChatRequest;

        public GetMessagesHandler
            (IGetMessagesRequest getMessagesRequest, 
            IGetChatRequest getChatRequest)
        {
            this.getMessagesRequest = getMessagesRequest;
            this.getChatRequest = getChatRequest;
        }

        public override async Task ProcessRequest(ProcessMessageTelegramRequest request)
        {
            var requestGet = new GetChatRequest() { expression = e => e.IdTelegram == request.id.ToString() };
            getChatRequest.Execute(requestGet);
            if (requestGet.IsError == false && requestGet.output!.Chats.Any())
            {
                var requestGetMessages = new GetMessagesRequest() { Expression = e=>e.ChatId==requestGet.output.Chats.First().Id};
                getMessagesRequest.Execute(requestGetMessages);
                request.chat = requestGet.output.Chats.First();
                if (requestGetMessages.IsError==false && requestGetMessages.output!.Messages.Any())
                {
                    request.messages = requestGetMessages.output.Messages.OrderBy(e=>e.NumberMessage).ToList();
                }
            }
            if (sucessor != null)
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}
