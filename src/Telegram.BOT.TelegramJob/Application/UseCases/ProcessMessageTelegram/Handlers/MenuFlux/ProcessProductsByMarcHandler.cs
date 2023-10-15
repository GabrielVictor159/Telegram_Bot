using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.BOT.Application.UseCases;
using Telegram.BOT.Application.UseCases.Products.GetProduct;
using Telegram.BOT.TelegramJob.Interfaces;

namespace Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram.Handlers.MenuFlux
{
    public class ProcessProductsByMarcHandler : Handler<ProcessMessageTelegramRequest>
    {
        private readonly IGetProductRequest getProductRequest;
        private readonly IMessageHelpers messageHelpers;
        public ProcessProductsByMarcHandler
            (IGetProductRequest getProductRequest,
            IMessageHelpers messageHelpers)
        {
            this.getProductRequest = getProductRequest;
            this.messageHelpers = messageHelpers;
        }

        public override async Task ProcessRequest(ProcessMessageTelegramRequest request)
        {
            var marcNames = request.Marcs.Select(e => e.Name).ToList();
            if (marcNames.Contains(request.text!))
            {
                var category = request.Marcs.Where(e => e.Name.ToLower().Equals(request.text!.ToLower())).FirstOrDefault();
                if (category != null)
                {
                    bool loop = true;
                    int page = 1;
                    while (loop)
                    {
                        var getProductsRequest = new GetProductRequest() { expression = e => e.MarcId == category.Id, page = page, pageSize = 3 };
                        await getProductRequest.Execute(getProductsRequest);
                        if (getProductsRequest.Products.Count == 0)
                        {
                            await request.client.SendTextMessageAsync(
                            chatId: request.id,
                            text: "Infelizmente ainda não temos produtos",
                            parseMode: ParseMode.MarkdownV2,
                            cancellationToken: new CancellationToken());
                            return;
                        }
                        foreach (var product in getProductsRequest.Products)
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
                        await Task.Delay(TimeSpan.FromSeconds(7));
                        bool flowContinue = await messageHelpers.ExecuteLoopPagination(7, request.client, request.id, "Gostaria de ver mais produtos ?");
                        if (flowContinue)
                        {
                            page++;
                        }
                        else
                        {
                            loop = false;
                        }

                    }

                    if (sucessor != null)
                    {
                        await sucessor.ProcessRequest(request);
                    }
                }
            }
        }
    }
}
