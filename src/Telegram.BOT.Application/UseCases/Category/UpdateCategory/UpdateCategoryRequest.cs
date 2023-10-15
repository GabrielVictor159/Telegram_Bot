using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Boundaries.Category;
using Telegram.BOT.Domain.Enums;
using Telegram.BOT.Domain.Logs;

namespace Telegram.BOT.Application.UseCases.Category.UpdateCategory
{
    public class UpdateCategoryRequest : Output<UpdateCategoryOutput>
    {
        public required Domain.Products.Category Category { get; init; }
        public List<Log> Logs { get; set; } = new();
        public void AddLog(LogType type, string message)
          => Logs.Add(Log.AddLog(type, message, "Update Category"));
    }
}
