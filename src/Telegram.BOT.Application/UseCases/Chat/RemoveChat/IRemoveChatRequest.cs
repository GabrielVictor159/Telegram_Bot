using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.BOT.Application.UseCases.Chat.RemoveChat
{
    public interface IRemoveChatRequest 
    {
        void Execute(RemoveChatRequest request);
    }
}
