using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.BOT.Application.UseCases;
using Telegram.BOT.Application.UseCases.Marc.GetMarc;

namespace Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram.Handlers.MenuFlux
{
    public class ProcessMenuMarcHandler : Handler<ProcessMessageTelegramRequest>
    {

        public override async Task ProcessRequest(ProcessMessageTelegramRequest request)
        {
            var marcNames = request.Marcs.Select(e => e.Name).ToList();
            if ( !marcNames.Contains(request.text!))
            {
                var options = new List<KeyboardButton>();
                marcNames.ForEach(e => options.Add(new KeyboardButton(e)));
                Bot.Types.Message pollMessage = await request.client.SendTextMessageAsync(
                 chatId: request.id,
                 text: request.MessageInitialMenu == "" ? "Escolha uma das marcas no menu abaixo" : request.MessageInitialMenu,
                 replyMarkup: new Bot.Types.ReplyMarkups.ReplyKeyboardMarkup(options),
                 cancellationToken: new CancellationToken());
            }
            else
            {
                if (sucessor != null)
                {
                    await sucessor.ProcessRequest(request);
                }
            }
        }
    }
}
