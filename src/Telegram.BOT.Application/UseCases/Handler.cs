using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Telegram.BOT.Application.UseCases
{
    public abstract class Handler<T>
    {
        protected Handler<T>? sucessor;
        public void SetSucessor(Handler<T> sucessor)
        {
            this.sucessor = sucessor;
        }
        public abstract Task ProcessRequest(T request);
    }
}