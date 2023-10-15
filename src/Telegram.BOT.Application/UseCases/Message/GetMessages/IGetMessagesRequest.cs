using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.UseCases.Marc.GetMarc;

namespace Telegram.BOT.Application.UseCases.Message.GetMessages
{
    public interface IGetMessagesRequest
    {
        void Execute(GetMessagesRequest request);
    }
}
