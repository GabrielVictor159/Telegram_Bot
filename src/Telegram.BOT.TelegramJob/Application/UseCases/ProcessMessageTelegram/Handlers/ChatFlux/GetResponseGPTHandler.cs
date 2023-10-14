using OpenAI.ObjectModels.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.UseCases;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.GetEnv;
using Telegram.BOT.TelegramJob.Interfaces;

namespace Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram.Handlers.ChatFlux
{
    public class GetResponseGPTHandler : Handler<ProcessMessageTelegramRequest>
    {
        private readonly IGPTService gPTService;
        private readonly IGetEnvRequest getEnvRequest;

        public GetResponseGPTHandler
            (IGPTService gPTService,
            IGetEnvRequest getEnvRequest)
        {
            this.gPTService = gPTService;
            this.getEnvRequest = getEnvRequest;
        }

        public override async Task ProcessRequest(ProcessMessageTelegramRequest request)
        {
            var message = new List<ChatMessage>();
            var requestEnvMarketDescription = new GetEnvRequest() { Key = "MARKET_DESCRIPTION" };
            await getEnvRequest.Execute(requestEnvMarketDescription);
            message.Add(ChatMessage.FromSystem(GetGPTScript()));
            message.Add(ChatMessage.FromSystem(requestEnvMarketDescription.output != null ? requestEnvMarketDescription.output.Value : ""));
            request.messages?.ForEach(e => 
            {
                if(e.Rule==Domain.Enums.MessageRules.USER)
                {
                    message.Add(ChatMessage.FromUser(e.Messaging));
                }
                else if(e.Rule == Domain.Enums.MessageRules.ASSISTANT)
                {
                    message.Add(ChatMessage.FromAssistant(e.Messaging));
                }
                else
                {
                    message.Add(ChatMessage.FromSystem(e.Messaging));
                }
            });
            message.Add(ChatMessage.FromUser(request.text??""));
            message.Add(ChatMessage.FromSystem($"{request.ProductsSearchLeveinstheim.Count} Produtos Encontrados"));
            (string response, bool sucessRequest) = await gPTService.ChatCompletation(message);
            if(sucessRequest)
            {
                request.responseChat = response;
            }
            else
            {
                if(request.ProductsSearchLeveinstheim.Count>0)
                {
                    request.responseChat = "Encontramos esses produtos com base nas suas mensagens!";
                }
                else
                {
                    request.responseChat = "Infelizmente não encontramos nenhum produto com base nas suas mensagens! \n Continue interagindo para encontrar o produto que deseja.";
                }
            }
            if (sucessor!=null)
            {
                await sucessor.ProcessRequest(request);
            }
        }
        public string GetGPTScript()
            => "Você esta intermediando a comunicação do meu sistema de vendas e o usuario dele, seu objetivo é responder amigavelmente as perguntas do usuario, apenas isso. JAMAIS TENTE DESCREVER OS PRODUTOS ENCONTRADOS";
    }
}
