using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases;

namespace Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram.Handlers.MenuFlux
{
    public class CreateChatHandler : Handler<ProcessMessageTelegramRequest>
    {
        private readonly IChatRepository chatRepository;

        public CreateChatHandler(IChatRepository chatRepository)
        {
            this.chatRepository = chatRepository;
        }

        public override async Task ProcessRequest(ProcessMessageTelegramRequest request)
        {
            var chat = new Domain.Chat.Chat() 
            {
                Id = Guid.NewGuid(), 
                IdTelegram = request.id.ToString(), 
                CreateDateTime = DateTime.Now,
                Username = request.userName ?? "",
                Location = request.userLocation != null ? request.userLocation.ToString()! : "",
                Description = request.userDescription ?? ""
            };
            chatRepository.Add(chat);
           Bot.Types.Message pollMessage = await request.client.SendTextMessageAsync(
           chatId: request.id,
           text: @"Apartir de agora você esta interagindo com o nosso chat, caso queira voltar para o menu escreva ""/exit""",  
           cancellationToken: new CancellationToken());
        }
    }
}
