using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Telegram.BOT.Application.Boundaries
{
    public interface IOutputPort<T>
    {
        void Standard(T output);
        void Error(string message);
        void NotFound(string message);
    }
}