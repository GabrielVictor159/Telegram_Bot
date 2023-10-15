using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;

namespace Telegram.BOT.Application.UseCases.Message.CreateMessage.Handlers
{
    public class VerifyMessageFourteenHandler : Handler<CreateMessageRequest>
    {
        private readonly IMessageRepository messageRepository;

        public VerifyMessageFourteenHandler(IMessageRepository messageRepository)
        {
            this.messageRepository = messageRepository;
        }

        public override async Task ProcessRequest(CreateMessageRequest request)
        {
            var entities = messageRepository.GetByFilter(e => e.ChatId == request.Message.ChatId).OrderBy(e=>e.NumberMessage).ToList();
            if (entities.Count>= 14)
            {
                messageRepository.Remove(entities.Last());
                entities.Remove(entities.Last());
               
            }
            request.Message.NumberMessage = entities.Count;
            if (sucessor!=null)
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}
