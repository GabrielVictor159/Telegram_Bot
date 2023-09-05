using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Telegram.BOT.Infrastructure.Database.Entities.Products
{
    public class Category
    {
        public Guid Id {get; set;}
        public string Name {get; set;} = "";
        public List<Marc> marcs {get; set;} = new();
    }
}