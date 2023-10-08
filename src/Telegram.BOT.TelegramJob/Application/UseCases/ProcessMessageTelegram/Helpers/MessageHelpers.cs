using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.BOT.TelegramJob.Interfaces;

namespace Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram.Helpers
{
    public class MessageHelpers : IMessageHelpers
    {

        public async Task<bool> ExecuteLoopPagination(int timer, ITelegramBotClient client, long idChat, string message)
        {
            int repeat = 0;
            bool loop = true;
            while (loop)
            {
                if (repeat == 4)
                {
                    loop = false;
                    await client.SendTextMessageAsync(
                    chatId: idChat,
                    text: "Infelizmente tivemos que finalizar a nossa interação",
                    parseMode: ParseMode.MarkdownV2,
                    cancellationToken: new CancellationToken());
                    return false;
                }

                Bot.Types.Message pollMessage = await client.SendPollAsync(
               chatId: idChat,
               question: message,
               options: new[]
               {
                    "Sim",
                    "Ainda não",
                    "Não quero voltar para o menu principal"
               },
               cancellationToken: new CancellationToken());

                await Task.Delay(TimeSpan.FromSeconds(7));

                Bot.Types.Poll poll = await client.StopPollAsync(
                chatId: pollMessage.Chat.Id,
                messageId: pollMessage.MessageId,
                cancellationToken: new CancellationToken());
                Console.WriteLine(JsonConvert.SerializeObject(poll));
                Bot.Types.PollOption[] selectOption = poll.Options.Where(e => e.VoterCount == 1).ToArray();
                if (selectOption.Length > 0)
                {
                    if (selectOption[0].Text.Equals("Sim"))
                    {
                        loop = false;
                        return true;
                    }
                    if (selectOption[0].Text.Equals("Não quero voltar para o menu principal"))
                    {
                        loop = false;
                        return false;
                    }
                }
                repeat++;
                await Task.Delay(TimeSpan.FromSeconds(timer));
            }
            return false;
        }
    }
}
