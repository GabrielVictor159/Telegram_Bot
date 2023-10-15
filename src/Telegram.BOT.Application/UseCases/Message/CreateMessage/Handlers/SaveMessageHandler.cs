using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;

namespace Telegram.BOT.Application.UseCases.Message.CreateMessage.Handlers
{
    public class SaveMessageHandler : Handler<CreateMessageRequest>
    {
        private readonly IMessageRepository messageRepository;

        public SaveMessageHandler(IMessageRepository messageRepository)
        {
            this.messageRepository = messageRepository;
        }

        public override async Task ProcessRequest(CreateMessageRequest request)
        {
            messageRepository.Add(request.Message);
            if(sucessor!=null) 
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}
