using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.BOT.Application.UseCases;

namespace Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram.Handlers.ChatFlux
{
    public class ResponseUserHandler : Handler<ProcessMessageTelegramRequest>
    {
        public override async Task ProcessRequest(ProcessMessageTelegramRequest request)
        {
            await request.client.SendTextMessageAsync(
                           chatId: request.id,
                           text: request.responseChat,
                           parseMode: ParseMode.MarkdownV2,
                           cancellationToken: new CancellationToken());

            foreach (var product in request.ProductsSearchLeveinstheim)
            {
                string pathFile = Path.Combine(Environment.GetEnvironmentVariable("ImagesPathByServiceInfra")!, product.Image);
                if (System.IO.File.Exists(pathFile))
                {
                    using (var stream = System.IO.File.OpenRead(pathFile))
                    {
                        Message message = await request.client.SendPhotoAsync(
                                       chatId: request.id,
                                       photo: InputFile.FromStream(stream),
                                       caption:
                                       $"Nome: {product.Name} \n" +
                                       $"Preço: R$ {product.Price} \n" +
                                       $"Descrição: {product.Description} \n"
                                       ,
                                       parseMode: ParseMode.Markdown,
                                       cancellationToken: new CancellationToken());
                    }
                }
            }
            if (sucessor!=null)
            {
                await sucessor.ProcessRequest(request); 
            }
        }
    }
}
