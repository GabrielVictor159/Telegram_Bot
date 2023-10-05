using ManagementServices.variables.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram;
using Telegram.BOT.TelegramJob.Interfaces;

namespace Telegram.BOT.TelegramJob.Job.Telegram
{
    public class BotTelegramService : IBotTelegramService
    {
        private readonly IEnvVariableRepository envVariableRepository;
        private readonly IProcessMessageTelegramRequest processMessageTelegramRequest;

        public BotTelegramService(IEnvVariableRepository envVariableRepository, IProcessMessageTelegramRequest processMessageTelegramRequest)
        {
            this.envVariableRepository = envVariableRepository;
            this.processMessageTelegramRequest = processMessageTelegramRequest;
        }

        public async Task StartAsync(CancellationToken token)
    {
        var receiverOptions = new ReceiverOptions()
        {
            AllowedUpdates = new UpdateType[]
            {
                UpdateType.Message,
                UpdateType.EditedMessage
            }
        };
       var tokenTelegram = envVariableRepository.Get("TELEGRAM_BOT_TOKEN");
            if(tokenTelegram==null)
            {
                bool loop = true;
                while(loop)
                {
                    Console.WriteLine("Token telegram not found");
                    tokenTelegram = envVariableRepository.Get("TELEGRAM_BOT_TOKEN");
                    Thread.Sleep(60 * 1000);
                    if(tokenTelegram !=null)
                    {
                        loop = false;
                    }
                }
            }
       var bot = new TelegramBotClient(tokenTelegram!=null? tokenTelegram.Value: "");
           bot.StartReceiving(UpdateHandler, ErrorHandler, receiverOptions);
        Console.WriteLine("Bot is running.");
        await Task.Delay(Timeout.Infinite, token); 
    }

    public async Task StopAsync(CancellationToken token)
    {
        Console.WriteLine("Bot is stopping.");
        await Task.CompletedTask;
    }

    private async Task ErrorHandler(ITelegramBotClient client, Exception exception, CancellationToken token)
    {
        Console.WriteLine("ErrorHandler: " + exception.Message);
        await Task.Delay(TimeSpan.FromSeconds(5), token);
    }

    private async Task UpdateHandler(ITelegramBotClient client, Update update, CancellationToken token)
    {
        Console.WriteLine("UpdateHandler");

        if (update.Type == UpdateType.Message)
        {
            if (update.Message!.Type == MessageType.Text)
            {
                var text = update.Message!.Text;
                var id = update.Message.Chat.Id;
                var userName = update.Message.Chat.FirstName + " " +update.Message.Chat.LastName;
                var userLocation = update.Message.Chat.Location;
                var userDescription = update.Message.Chat.Description;
                Console.WriteLine($"Message: {text}/ Id: {id}/ Username: {userName}/ Location: {userLocation}/ Description: {userDescription}");
                    await processMessageTelegramRequest.Execute
                        (new() {client = client, id = id, text = text, userName = userName, 
                            userLocation = userLocation, userDescription = userDescription });
            }
        }

        await Task.Delay(TimeSpan.FromSeconds(1), token);
    }
    }
  
}
