using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Boundaries.Products;
using Telegram.BOT.Domain.Enums;
using Telegram.BOT.Domain.Logs;
using Telegram.BOT.Domain.Products;

namespace Telegram.BOT.Application.UseCases.Products.CreateProduct;

public class CreateProductRequest : Output<ProductOutput>
{
   public required Domain.Products.Product Product { get; init; }
   public required byte[] Image { get; init; }
   public ProductOutput? ProductOutput { get; set; }
   public List<Log> Logs { get; set; } = new();
   public List<Groups> groups100 { get; set; } = new();
   public List<Groups> groups75 { get; set; } = new();
   public List<Groups> groups50 { get; set; } = new();
   public List<Groups> groups25 { get; set; } = new ();
    public void AddLog(LogType type, string message)
           => Logs.Add(Log.AddLog(type, message, "Create Product"));
}
