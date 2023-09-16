using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Telegram.BOT.Infrastructure.Database.Entities.Products
{
    public class Marc
    {
        public Guid Id {get; set;}
        public string Name {get; set;} = "";
        public virtual Category? Category {get; set;}
        public Guid? CategoryId {get; set;}
        public List<Product> products {get;set;} =new();
    }
}