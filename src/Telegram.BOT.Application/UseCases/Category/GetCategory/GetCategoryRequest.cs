using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Boundaries.Category;
using Telegram.BOT.Domain.Enums;
using Telegram.BOT.Domain.Logs;

namespace Telegram.BOT.Application.UseCases.Category.GetCategory;

public class GetCategoryRequest : Output<GetCategoryOutput>
{
    public string Name { get; init; } = "";
    public List<Domain.Products.Category> Categories { get; set; } = new();
    public List<Log> Logs { get; set; } = new();
    public int page { get; init; } = 1;
    public int pageSize { get; init; } = 10;
    public void AddLog(LogType type, string message)
           => Logs.Add(Log.AddLog(type, message, "Get Category"));
}
