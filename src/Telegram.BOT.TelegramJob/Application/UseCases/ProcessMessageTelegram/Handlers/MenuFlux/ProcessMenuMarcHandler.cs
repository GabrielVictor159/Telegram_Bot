using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.BOT.Application.UseCases;
using Telegram.BOT.Application.UseCases.Marc.GetMarc;

namespace Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram.Handlers.MenuFlux
{
    public class ProcessMenuMarcHandler : Handler<ProcessMessageTelegramRequest>
    {
        private readonly IGetMarcRequest getMarcRequest;

        public ProcessMenuMarcHandler(IGetMarcRequest getMarcRequest)
        {
            this.getMarcRequest = getMarcRequest;
        }

        public override async Task ProcessRequest(ProcessMessageTelegramRequest request)
        {
            var requestMarcs = new GetMarcRequest() { CategoryId = request.idCategory, pageSize = int.MaxValue };
            await getMarcRequest.Execute(requestMarcs);
            string[] options = requestMarcs.Marcs.Select(e => e.Name).ToArray();
            if (options.Length > 1)
            {
                Bot.Types.Message pollMessage = await request.client.SendPollAsync(
                    chatId: request.id,
                    question: "Nos possuimos essas marcas de produto nessa categoria, por favor selecione a que mais te interessa",
                    options: options,
                    cancellationToken: new CancellationToken());

                await Task.Delay(TimeSpan.FromSeconds(7));

                Bot.Types.Poll poll = await request.client.StopPollAsync(
                chatId: pollMessage.Chat.Id,
                messageId: pollMessage.MessageId,
                cancellationToken: new CancellationToken());
                Console.WriteLine(JsonConvert.SerializeObject(poll));
                Bot.Types.PollOption[] selectOption = poll.Options.Where(e => e.VoterCount == 1).ToArray();
                if (selectOption.Length > 0)
                {
                    var optionSelect = selectOption[0].Text;
                    var item = requestMarcs.Marcs.Where(e => e.Name.Equals(optionSelect)).FirstOrDefault();
                    if (item != null)
                    {
                        request.idMarc = item.Id;
                    }
                }
            }
            else if (options.Length == 1)
            {
                request.idMarc = requestMarcs.Marcs[0].Id;
            }
            else
            {
                await request.client.SendTextMessageAsync(
                        chatId: request.id,
                        text: "Infelizmente ainda não temos produtos",
                        parseMode: ParseMode.MarkdownV2,
                        cancellationToken: new CancellationToken());
                return;
            }
            if (sucessor != null)
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}
