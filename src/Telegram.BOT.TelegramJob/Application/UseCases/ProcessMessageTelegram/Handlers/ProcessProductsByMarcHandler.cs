using ManagementServices.variables.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.BOT.Application.UseCases;
using Telegram.BOT.Application.UseCases.Products.GetProduct;
using Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram.Helpers;
using Telegram.BOT.TelegramJob.Interfaces;
using Telegram.Bots;
using Telegram.Bots.Http;

namespace Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram.Handlers
{
    public class ProcessProductsByMarcHandler : Handler<ProcessMessageTelegramRequest>
    {
        private readonly IGetProductRequest getProductRequest;
        private readonly IEnvVariableRepository envVariableRepository;
        private readonly IMessageHelpers messageHelpers;
        public ProcessProductsByMarcHandler
            (IGetProductRequest getProductRequest, 
            IEnvVariableRepository envVariableRepository,
            IMessageHelpers messageHelpers)
        {
            this.getProductRequest = getProductRequest;
            this.envVariableRepository = envVariableRepository;
            this.messageHelpers = messageHelpers;
        }

        public override async Task ProcessRequest(ProcessMessageTelegramRequest request)
        {
            bool loop = true;
            int page = 1;
            var linkSystem = envVariableRepository.Get("URLSYSTEM");
            if (linkSystem == null)
            {
                while (loop)
                {
                    var getProductsRequest = new GetProductRequest() { expression = (e => e.MarcId == request.idMarc), page = page, pageSize = 3 };
                    await getProductRequest.Execute(getProductsRequest);
                    if(getProductsRequest.Products.Count ==0)
                    {
                        await request.client.SendTextMessageAsync(
                        chatId: request.id,
                        text: "Infelizmente ainda não temos produtos",
                        parseMode: ParseMode.MarkdownV2,
                        cancellationToken: new CancellationToken());
                        return;
                    }
                    foreach(var product in getProductsRequest.Products) 
                    {
                        await request.client.SendPhotoAsync(
                                               chatId: request.id,
                                               photo: InputFile.FromUri($"{linkSystem}/{product.Image}"),
                                               caption: 
                                               $"<h>{product.Name}</h>"+
                                               $"<p>Preço: R$ {product.Price}</p>"+
                                               $"<p>Descrição: {product.Description}</p>"
                                               ,
                                               parseMode: ParseMode.Html,
                                               cancellationToken: new CancellationToken());
                    }
                    await Task.Delay(TimeSpan.FromSeconds(7));
                    await messageHelpers.ExecuteLoopPagination(20, request.client, page, request.id, "Gostaria de ver mais produtos ?", request, loop);
                }
            }
            if (sucessor != null)
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}
