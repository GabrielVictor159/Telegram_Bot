using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Telegram.BOT.Infrastructure
{
    public class InfrastructureException : Exception
    {
        public InfrastructureException(string businessMessage)
             : base(businessMessage)
        {
        }


    }
}