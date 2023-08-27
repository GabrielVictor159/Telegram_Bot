using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Telegram.BOT.Application
{
    public class ApplicationException : Exception
    {
        public ApplicationException(string businessMessage)
             : base(businessMessage)
        {
        }
    }
}