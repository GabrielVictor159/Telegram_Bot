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
using Telegram.BOT.Application.UseCases.Category.GetCategory;
using Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram.Helpers;

namespace Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram.Handlers.MenuFlux
{
    public class ProcessMenuCategoryHandler : Handler<ProcessMessageTelegramRequest>
    {

        public override async Task ProcessRequest(ProcessMessageTelegramRequest request)
        {
            var categoryNames = request.Categories.Select(e => e.Name).ToList();
            var marcNames = request.Marcs.Select(e => e.Name).ToList();

            if (!categoryNames.Contains(request.text!) && !marcNames.Contains(request.text!))
            {
                var options = MessageHelpers.CreateKeyboardOptions(categoryNames, 3);
                var replyMarkup = new ReplyKeyboardMarkup(options);
                Bot.Types.Message pollMessage = await request.client.SendTextMessageAsync(
                    chatId: request.id,
                    text: request.MessageInitialMenu == "" ? "Escolha uma das categorias no menu abaixo" : request.MessageInitialMenu,
                    replyMarkup: replyMarkup,
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
