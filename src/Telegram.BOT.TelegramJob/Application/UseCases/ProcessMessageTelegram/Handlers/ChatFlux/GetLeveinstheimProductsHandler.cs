using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.UseCases;
using Telegram.BOT.Application.UseCases.Chat.GetChat;
using Telegram.BOT.Application.UseCases.Message.GetMessages;
using Telegram.BOT.Application.UseCases.Products.GetByLeveinstheim;
using Telegram.BOT.Domain.Enums;
using Telegram.BOT.TelegramJob.Interfaces;

namespace Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram.Handlers.ChatFlux
{
    public class GetLeveinstheimProductsHandler : Handler<ProcessMessageTelegramRequest>
    {
        private readonly IGetByLeveinstheimRequest getByLeveinstheimRequest;

        public GetLeveinstheimProductsHandler
            (IGetByLeveinstheimRequest getByLeveinstheimRequest)
        {
            this.getByLeveinstheimRequest = getByLeveinstheimRequest;
        }

        public override async Task ProcessRequest(ProcessMessageTelegramRequest request)
        {
            string inputString = "";
            request.messages?.ForEach(e =>
            {
                if (e.Rule == MessageRules.USER)
                {
                    inputString += $" {e.Messaging}";
                }
            });
            inputString += request.text;
            var requestProducts = new GetByLeveinstheimRequest() { inputString = inputString, numberItems = 3 };
            await getByLeveinstheimRequest.Execute(requestProducts);
            if(requestProducts.IsError==false && requestProducts.output!=null)
            {
                request.ProductsSearchLeveinstheim = requestProducts.output.Products;
            }
            if(sucessor != null) 
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}
