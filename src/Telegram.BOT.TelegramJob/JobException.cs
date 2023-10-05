using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.BOT.TelegramJob
{
    public class JobException : Exception
    {
        public JobException(string businessMessage)
             : base(businessMessage)
        {
        }
    }
}
