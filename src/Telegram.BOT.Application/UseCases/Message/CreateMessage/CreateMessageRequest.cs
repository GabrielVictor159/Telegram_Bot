﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Enums;
using Telegram.BOT.Domain.Logs;

namespace Telegram.BOT.Application.UseCases.Message.CreateMessage
{
    public class CreateMessageRequest : Output<string>
    {
        public required Domain.Chat.Message Message { get; init; }
        public List<Log> Logs { get; set; } = new();
        public void AddLog(LogType type, string message)
               => Logs.Add(Log.AddLog(type, message, "Create Message"));
    }
}
