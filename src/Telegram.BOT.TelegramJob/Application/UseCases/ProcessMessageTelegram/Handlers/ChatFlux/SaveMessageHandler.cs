using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.UseCases;
using Telegram.BOT.Application.UseCases.Message.CreateMessage;

namespace Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram.Handlers.ChatFlux
{
    public class SaveMessageHandler : Handler<ProcessMessageTelegramRequest>
    {
        private readonly ICreateMessageRequest createMessageRequest;

        public SaveMessageHandler(ICreateMessageRequest createMessageRequest)
        {
            this.createMessageRequest = createMessageRequest;
        }

        public override async Task ProcessRequest(ProcessMessageTelegramRequest request)
        {
            if (request.chat != null)
            {
                var messageUserRequest = new CreateMessageRequest()
                {
                    Message = new()
                    {
                        Id = Guid.NewGuid(),
                        ChatId = request.chat.Id,
                        Messaging = request.text!,
                        Rule = Domain.Enums.MessageRules.USER,
                        NumberMessage = request.messages != null ? request.messages.Count : 0
                    }
                };
                var messageSystem1Request = new CreateMessageRequest()
                {
                    Message = new()
                    {
                        Id = Guid.NewGuid(),
                        ChatId = request.chat.Id,
                        Messaging = $"{request.ProductsSearchLeveinstheim.Count} Produtos Encontrados",
                        Rule = Domain.Enums.MessageRules.SYSTEM,
                        NumberMessage = request.messages != null ? request.messages.Count + 1 : 1
                    }
                };
                var messageSystem2Request = new CreateMessageRequest()
                {
                    Message = new()
                    {
                        Id = Guid.NewGuid(),
                        ChatId = request.chat.Id,
                        Messaging = request.responseChat,
                        Rule = Domain.Enums.MessageRules.ASSISTANT,
                        NumberMessage = request.messages != null ? request.messages.Count+2 : 2
                    }
                };
                

                await createMessageRequest.Execute(messageUserRequest);
                await createMessageRequest.Execute(messageSystem1Request);
                await createMessageRequest.Execute(messageSystem2Request);
            }
            if(sucessor != null) 
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}
