using ManagementServices.variables.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.BOT.Application.UseCases;
using Telegram.BOT.Application.UseCases.Products.GetProduct;
using Telegram.Bots.Http;

namespace Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram.Handlers
{
    public class ProcessProductsByMarcHandler : Handler<ProcessMessageTelegramRequest>
    {
        private readonly IGetProductRequest getProductRequest;
        private readonly IEnvVariableRepository envVariableRepository;

        public ProcessProductsByMarcHandler(IGetProductRequest getProductRequest, IEnvVariableRepository envVariableRepository)
        {
            this.getProductRequest = getProductRequest;
            this.envVariableRepository = envVariableRepository;
        }

        public override async Task ProcessRequest(ProcessMessageTelegramRequest request)
        {
            bool loop = true;
            int page = 1;
            while (loop) 
            {
                var getProductsRequest = new GetProductRequest() {expression = (e=>e.MarcId == request.idMarc), page = page, pageSize = 3};
                
            }
        }
    }
}
