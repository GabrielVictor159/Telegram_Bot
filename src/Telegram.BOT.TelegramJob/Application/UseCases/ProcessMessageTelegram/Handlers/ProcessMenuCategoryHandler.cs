using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.BOT.Application.UseCases;
using Telegram.BOT.Application.UseCases.Category.GetCategory;

namespace Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram.Handlers
{
    public class ProcessMenuCategoryHandler : Handler<ProcessMessageTelegramRequest>
    {
        private readonly IGetCategoryRequest getCategoryRequest;

        public ProcessMenuCategoryHandler(IGetCategoryRequest getCategoryRequest)
        {
            this.getCategoryRequest = getCategoryRequest;
        }

        public override async Task ProcessRequest(ProcessMessageTelegramRequest request)
        {
            var requestCategories = new GetCategoryRequest() { pageSize = int.MaxValue};
            await getCategoryRequest.Execute(requestCategories);
            string[] options = requestCategories.Categories.Select(e => e.Name).ToArray();
            Bot.Types.Message pollMessage = await request.client.SendPollAsync(
                chatId: request.id,
                question:  "Nos possuimos essas categorias de produtos, por favor escolha a que te interessa" ,
                options: options,
                cancellationToken: new CancellationToken());

            await Task.Delay(TimeSpan.FromSeconds(20));

            Bot.Types.Poll poll = await request.client.StopPollAsync(
            chatId: pollMessage.Chat.Id,
            messageId: pollMessage.MessageId,
            cancellationToken: new CancellationToken());
            Console.WriteLine(JsonConvert.SerializeObject(poll));
            Bot.Types.PollOption[] selectOption = poll.Options.Where(e => e.VoterCount == 1).ToArray();
            if(selectOption.Length > 0)
            {
                var optionSelect = selectOption[0].Text;
                var item = requestCategories.Categories.Where(e => e.Name.Equals(optionSelect)).FirstOrDefault();
                if(item != null) 
                {
                    request.idCategory = item.Id;
                }
            }
            if(sucessor != null) 
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}
