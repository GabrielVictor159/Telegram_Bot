using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Boundaries.Marc;
using Telegram.BOT.Domain.Enums;
using Telegram.BOT.Domain.Logs;

namespace Telegram.BOT.Application.UseCases.Marc.GetMarc;

public class GetMarcRequest : Output<GetMarcOutput>
{
    public string Name { get; init; } = "";
    public Guid CategoryId { get; init; }
    public List<Domain.Products.Marc> Marcs { get; set; } = new();
    public List<Log> Logs { get; set; } = new();
    public int page { get; init; } = 1;
    public int pageSize { get; init; } = 10;
    public void AddLog(LogType type, string message)
           => Logs.Add(Log.AddLog(type, message, "Get Marc"));
}
