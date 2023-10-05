using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.BOT.TelegramJob.Interfaces
{
    public interface IBotTelegramService
    {
        Task StartAsync(CancellationToken token);
        Task StopAsync(CancellationToken token);
    }
}
