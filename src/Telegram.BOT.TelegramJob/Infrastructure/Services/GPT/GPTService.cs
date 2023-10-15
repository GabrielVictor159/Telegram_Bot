using OpenAI.Managers;
using OpenAI;
using OpenAI.ObjectModels.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementServices.variables.Application.Interfaces;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.GetEnv;
using OpenAI.Interfaces;
using OpenAI.ObjectModels;
using Telegram.BOT.TelegramJob.Interfaces;

namespace Telegram.BOT.TelegramJob.Infrastructure.Services.GPT
{
    public class GPTService : IGPTService
    {
        private readonly IGetEnvRequest getEnvRequest;
        public GPTService(IGetEnvRequest getEnvRequest)
        {
            this.getEnvRequest = getEnvRequest;
        }

        public async Task<(string, bool)> ChatCompletation(List<ChatMessage> messages)
        {
            OpenAIService openAIService;
             var request = new GetEnvRequest() { Key = "GPT_TOKEN" };
             await getEnvRequest.Execute(request);
            if (request.IsError == false && request.output != null)
            {
                openAIService = new OpenAIService(new OpenAiOptions()
                {
                    ApiKey = request.output.Value
                });

                var completionResult = await openAIService!.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
                {
                    Messages = messages,
                    Model = Models.Gpt_4,
                    MaxTokens = 300
                });

                if (completionResult.Successful)
                {
                    return (completionResult.Choices.First().Message.Content, true);
                }
                else
                {
                    return ("", false);
                }
            }
            else
            {
                return ("", false);
            }
        }
    }
}
