using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Boundaries.Products;
using Telegram.BOT.Domain.Enums;
using Telegram.BOT.Domain.Logs;

namespace Telegram.BOT.Application.UseCases.Products.GetByLeveinstheim
{
    public class GetByLeveinstheimRequest : Output<GetByLeveinstheimOutput>
    {
        public required string inputString { get; init; }
        public required int numberItems { get; init; }
        public List<Domain.Products.Groups> Groups { get; set; } = new();
        public List<Domain.Products.Product> Products { get; set; } = new();
        public List<Log> Logs { get; set; } = new();
        public void AddLog(LogType type, string message)
               => Logs.Add(Log.AddLog(type, message, "Get By Leveinstheim Product"));
    }
}
