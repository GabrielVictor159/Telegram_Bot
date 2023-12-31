﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Boundaries.Products;
using Telegram.BOT.Domain.Enums;
using Telegram.BOT.Domain.Logs;
using Telegram.BOT.Domain.Products;

namespace Telegram.BOT.Application.UseCases.Products.DeleteProduct;

public class DeleteProductRequest : Output<DeleteProductOutput>
{
    public required Guid Id {get; init;}
    public Product? Product { get; set;}
    public List<Log> Logs { get; set; } = new();
    public void AddLog(LogType type, string message)
           => Logs.Add(Log.AddLog(type, message, "Delete Product"));
}
