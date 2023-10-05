using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram.Helpers
{
    public class MessageHelpers
    {
        public void ExecuteLoopPagination(int timer, ITelegramBotClient client, int page)
        {
            int repeat = 0;
            bool loop = true;
            while (loop)
            {

                repeat++;
                if(repeat==3)
                {
                    loop = false;
                }
            }
        }
    }
}
