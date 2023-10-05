using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.BOT.TelegramJob.Interfaces;

namespace Telegram.BOT.TelegramJob.Job.Telegram
{
    public class BotTelegramService : IBotTelegramService
    {

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
        var bot = new TelegramBotClient("");
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
                var username = update.Message.Chat.Username;
                Console.WriteLine($"Message: {text}/ Id: {id}/ Username: {username}");
                await client.SendTextMessageAsync(id, "Bot received your message.");
            }
        }

        await Task.Delay(TimeSpan.FromSeconds(1), token);
    }
    }
  
}
