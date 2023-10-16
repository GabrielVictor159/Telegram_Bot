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
using Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram.Helpers;

namespace Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram.Handlers.MenuFlux
{
    public class ProcessMenuMarcHandler : Handler<ProcessMessageTelegramRequest>
    {

        public override async Task ProcessRequest(ProcessMessageTelegramRequest request)
        {
            var marcNames = request.Marcs.Select(e => e.Name).ToList();
            var category = request.Categories.Where(e=>e.Name==request.text).FirstOrDefault();
            if ( !marcNames.Contains(request.text!))
            {
                if (category != null)
                {
                    var marcsCategory = request.Marcs.Where(e=>e.CategoryId==category.Id).ToList();
                    if (marcsCategory.Any())
                    {
                        var marcCategoryNames = request.Marcs.Select(e => e.Name).ToList();
                        var options = MessageHelpers.CreateKeyboardOptions(marcCategoryNames, 3);
                        var replyMarkup = new ReplyKeyboardMarkup(options);
                        Bot.Types.Message pollMessage = await request.client.SendTextMessageAsync(
                         chatId: request.id,
                         text: request.MessageInitialMenu == "" ? "Escolha uma das marcas no menu abaixo" : request.MessageInitialMenu,
                         replyMarkup: replyMarkup,
                         cancellationToken: new CancellationToken());
                    }
                    else
                    {
                        await request.client.SendTextMessageAsync(
                            chatId: request.id,
                            text: "Infelizmente não temos nenhuma marca relacionada a essa categoria",
                            parseMode: ParseMode.MarkdownV2,
                            cancellationToken: new CancellationToken());
                    }
                }
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
