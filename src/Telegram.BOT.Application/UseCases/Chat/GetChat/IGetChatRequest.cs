﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.BOT.Application.UseCases.Chat.GetChat
{
    public interface IGetChatRequest
    {
        void Execute(GetChatRequest request);
    }
}
