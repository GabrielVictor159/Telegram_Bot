using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Enums;
using Telegram.BOT.Domain.Logs;
using Telegram.BOT.Domain.Products;

namespace Telegram.BOT.Application.UseCases.Products.GetProduct;

public class GetProductRequest
{
    public string Name { get; init; } = "";
    public string Tag { get; init; } = "";
    public DateTime DeDate { get; init; } = DateTime.MinValue;
    public DateTime AteDate { get; init; } = DateTime.MaxValue;
    public string Description { get; init; } = "";
    public List<Log> Logs { get; set; } = new();
    public List<Product> Products { get; set; } = new();
    public int page { get; init; } = 1;
    public int pageSize { get; init; } = 10;
    public void AddLog(LogType type, string message)
           => Logs.Add(Log.AddLog(type, message, "Get Products"));
}
