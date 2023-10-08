using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.BOT.Application.UseCases;
using Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram.Handlers.MenuFlux;
using Telegram.Bots.Types;

namespace Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram.Handlers
{
#pragma warning disable 4014
    public class ProcessInitialMessageHandler : Handler<ProcessMessageTelegramRequest>
    {
        private readonly ProcessMenuCategoryHandler processMenuCategoryHandler;
        public ProcessInitialMessageHandler
            (ProcessMenuCategoryHandler processMenuCategoryHandler,
            ProcessMenuMarcHandler processMenuMarcHandler,
            ProcessProductsByMarcHandler processProductsByMarcHandler)
        {
            processMenuCategoryHandler.SetSucessor
                (
                    processMenuMarcHandler.SetSucessor
                    (
                        processProductsByMarcHandler.SetSucessor(this)
                    )
                );
            this.processMenuCategoryHandler= processMenuCategoryHandler;
        }
        public override async Task ProcessRequest(ProcessMessageTelegramRequest request)
        {
           if(request.chat==null)
            {
                Bot.Types.Message pollMessage = await request.client.SendPollAsync(
                chatId: request.id,
                question: request.MessageInitialMenu==""?"Ola como gostaria do seu atendimento ?": request.MessageInitialMenu,
                options: new[]
                {
                    "Menu",
                    "Chat"
                },
                cancellationToken: new CancellationToken());
                
                await Task.Delay(TimeSpan.FromSeconds(7));

                Bot.Types.Poll poll = await request.client.StopPollAsync(
                chatId: pollMessage.Chat.Id,
                messageId: pollMessage.MessageId,
                cancellationToken: new CancellationToken());
                Console.WriteLine(JsonConvert.SerializeObject(poll));
                Bot.Types.PollOption[] selectOption = poll.Options.Where(e => e.VoterCount > 0).ToArray();
                if(selectOption.Length > 0) 
                {
                    if (selectOption[0].Text.Equals("Menu")) 
                    {
                        await processMenuCategoryHandler.ProcessRequest(request);
                    }
                    else
                    {

                    }
                }
                return;
            }
           if(sucessor!=null)
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
#pragma warning restore 4014
}
