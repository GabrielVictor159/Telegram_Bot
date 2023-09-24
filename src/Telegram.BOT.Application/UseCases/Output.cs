using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Boundaries.Marc;

namespace Telegram.BOT.Application.UseCases
{
    public abstract class Output<T>
    {
        public bool IsError { get; set; } = false;
        public string ErrorMessage { get; set; } = "";
        public T? output { get; set; }
    }
}
