using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases;
using Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram.Handlers.ChatFlux;
using Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram.Handlers.MenuFlux;

namespace Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram.Handlers
{
    public class InitialSearchChatHandler : Handler<ProcessMessageTelegramRequest>
    {
        private readonly IChatRepository chatRepository;
        private readonly AllMenuOptionsHandler allMenuOptionsHandler;
        private readonly ProcessExitMessageHandler processExitMessageHandler;
        public InitialSearchChatHandler
            (IChatRepository chatRepository,
            AllMenuOptionsHandler allMenuOptionsHandler,
            ProcessInitialMessageHandler processInitialMessageHandler,
            ProcessExitMessageHandler processExitMessageHandler,
            GetMessagesHandler getMessagesHandler,
            GetLeveinstheimProductsHandler getLeveinstheimProductsHandler,
            GetResponseGPTHandler getResponseGPTHandler,
            ResponseUserHandler responseUserHandler,
            SaveMessageHandler saveMessageHandler)
        {
            allMenuOptionsHandler.SetSucessor(processInitialMessageHandler);
            this.chatRepository = chatRepository;
            this.allMenuOptionsHandler = allMenuOptionsHandler;
            processExitMessageHandler.SetSucessor
                (getMessagesHandler.SetSucessor
                (getLeveinstheimProductsHandler.SetSucessor
                (getResponseGPTHandler.SetSucessor
                (responseUserHandler.SetSucessor
                (saveMessageHandler)))));
            this.processExitMessageHandler = processExitMessageHandler;
        }

        public override async Task ProcessRequest(ProcessMessageTelegramRequest request)
        {
            Console.WriteLine("InitialSearchChatHandler");
            var chat = chatRepository.GetByFilter(e => e.IdTelegram == request.id.ToString()).FirstOrDefault();
            if(chat != null) 
            {
                request.chat = chat;
                await processExitMessageHandler.ProcessRequest(request);
            }
            else
            {
                await allMenuOptionsHandler.ProcessRequest(request);
            }
        }
    }
}
