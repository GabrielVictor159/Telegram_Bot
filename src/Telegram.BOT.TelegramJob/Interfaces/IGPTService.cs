using OpenAI.ObjectModels.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.BOT.TelegramJob.Interfaces
{
    public interface IGPTService
    {
        Task<(string, bool)> ChatCompletation(List<ChatMessage> messages);
    }
}
