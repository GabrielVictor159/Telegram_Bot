using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram;
using Telegram.Bot;

namespace Telegram.BOT.TelegramJob.Interfaces
{
    public interface IMessageHelpers
    {
        Task<bool> ExecuteLoopPagination(int timer, ITelegramBotClient client, long idChat, string message);
    }
}
