using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.BOT.Application.UseCases;
using Telegram.Bots.Types;

namespace Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram.Handlers.MenuFlux
{
#pragma warning disable 4014
    public class ProcessInitialMessageHandler : Handler<ProcessMessageTelegramRequest>
    {
        private readonly ProcessMenuCategoryHandler processMenuCategoryHandler;
        private readonly CreateChatHandler createChatHandler;
        public ProcessInitialMessageHandler
            (ProcessMenuCategoryHandler processMenuCategoryHandler,
            ProcessMenuMarcHandler processMenuMarcHandler,
            ProcessProductsByMarcHandler processProductsByMarcHandler,
            CreateChatHandler createChatHandler)
        {
            processMenuCategoryHandler.SetSucessor
                (
                    processMenuMarcHandler.SetSucessor
                    (
                        processProductsByMarcHandler.SetSucessor(this)
                    )
                );
            this.processMenuCategoryHandler = processMenuCategoryHandler;
            this.createChatHandler = createChatHandler;
        }
        public override async Task ProcessRequest(ProcessMessageTelegramRequest request)
        {
            Console.WriteLine("ProcessInitialMessageHandler");
            var categoryNames = request.Categories.Select(e => e.Name).ToList();
            var marcNames = request.Marcs.Select(e => e.Name).ToList();
            if (request.text == "Menu" || categoryNames.Contains(request.text!) || marcNames.Contains(request.text!))
            {
                await processMenuCategoryHandler.ProcessRequest(request);
                
            }
            else if (request.text == "Chat")
            {
                await createChatHandler.ProcessRequest(request);
                
            }
            else
            {
                Bot.Types.Message pollMessage = await request.client.SendTextMessageAsync(
                chatId: request.id,
                text: request.MessageInitialMenu == "" ? "Ola como gostaria do seu atendimento? \n Selecione a opção no menu abaixo e caso escolha a opção menu outros menus aparecerão " : request.MessageInitialMenu,
                replyMarkup: new Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
                (
                    new List<List<KeyboardButton>>()
                    {
                        new List<KeyboardButton>()
                        {
                            new KeyboardButton("Menu"),
                            new KeyboardButton("Chat")
                        }
                    }
                ),
                cancellationToken: new CancellationToken());
                string pathImage1 = "/app/Application/UseCases/ProcessMessageTelegram/References/Images/MenuSelectAndroid.png";
                string pathImage2 = "/app/Application/UseCases/ProcessMessageTelegram/References/Images/MenuSelectPC.png";
                if (System.IO.File.Exists(pathImage1) && System.IO.File.Exists(pathImage2))
                {
                    using (var stream = System.IO.File.OpenRead(pathImage1))
                    {
                        Bot.Types.Message message = await request.client.SendPhotoAsync(
                                       chatId: request.id,
                                       photo: Bot.Types.InputFile.FromStream(stream),
                                       caption: "Menu no celular",
                                       parseMode: (Bot.Types.Enums.ParseMode?)ParseMode.Markdown,
                                       cancellationToken: new CancellationToken());
                    }
                    using (var stream = System.IO.File.OpenRead(pathImage2))
                    {
                        Bot.Types.Message message = await request.client.SendPhotoAsync(
                                       chatId: request.id,
                                       photo: Bot.Types.InputFile.FromStream(stream),
                                       caption: "Menu no computador",
                                       parseMode: (Bot.Types.Enums.ParseMode?)ParseMode.Markdown,
                                       cancellationToken: new CancellationToken());
                    }
                }
            }

        }
    }
#pragma warning restore 4014
}
