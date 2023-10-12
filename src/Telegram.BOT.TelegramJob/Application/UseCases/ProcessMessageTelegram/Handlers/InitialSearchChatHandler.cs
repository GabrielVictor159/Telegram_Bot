using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases;
using Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram.Handlers.MenuFlux;

namespace Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram.Handlers
{
    public class InitialSearchChatHandler : Handler<ProcessMessageTelegramRequest>
    {
        private readonly IChatRepository chatRepository;
        private readonly AllMenuOptionsHandler allMenuOptionsHandler;

        public InitialSearchChatHandler
            (IChatRepository chatRepository,
            AllMenuOptionsHandler allMenuOptionsHandler,
            ProcessInitialMessageHandler processInitialMessageHandler)
        {
            allMenuOptionsHandler.SetSucessor(processInitialMessageHandler);
            this.chatRepository = chatRepository;
            this.allMenuOptionsHandler = allMenuOptionsHandler;
        }

        public override async Task ProcessRequest(ProcessMessageTelegramRequest request)
        {
            Console.WriteLine("InitialSearchChatHandler");
            var chat = chatRepository.GetByFilter(e => e.IdTelegram == request.id.ToString()).FirstOrDefault();
            if(chat != null) 
            {
                request.chat = chat;
               
            }
            else
            {
                await allMenuOptionsHandler.ProcessRequest(request);
            }
        }
    }
}
