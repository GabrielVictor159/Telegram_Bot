using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.UseCases.Products.CreateProduct;

namespace Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram
{
    public interface IProcessMessageTelegramRequest
    {
        Task Execute(ProcessMessageTelegramRequest request);
    }
}
