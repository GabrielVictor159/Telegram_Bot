using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Telegram.BOT.WebMVC
{
    public class WebMVCException : Exception
    {
        public WebMVCException(string businessMessage)
             : base(businessMessage)
        {
        }


    }
}