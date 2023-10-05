using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.BOT.Application.UseCases;
using Telegram.BOT.Domain.Chat;
using Telegram.BOT.Domain.Enums;
using Telegram.BOT.Domain.Logs;

namespace Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram
{
    public class ProcessMessageTelegramRequest 
    {
        public required ITelegramBotClient client { get; set; }
        public string? text { get; set; }
        public required long id { get; set; }
        public string? userName { get; set; }
        public ChatLocation? userLocation { get; set; }
        public string? userDescription { get; set; }
        public Update? update { get; set; }
        public BOT.Domain.Chat.Chat? chat { get; set; }
        public List<BOT.Domain.Chat.Message>? messages { get; set; }
        public string MessageInitialMenu { get; set; } = "";
        public Guid idCategory { get; set; }
        public Guid idMarc { get; set; }
        public List<Log> Logs { get; set; } = new();
        public void AddLog(LogType type, string message)
               => Logs.Add(Log.AddLog(type, message, "Process Message Telegram"));
    }
}
