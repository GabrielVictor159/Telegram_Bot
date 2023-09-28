using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.BOT.Infrastructure.Database.Entities.Products
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Image { get; set; } = "";
        public string Tags { get; set; } = "";
        public DateTime CreateDate { get;  set; }
        public double Price { get; set; } = 0;
        public virtual Marc? Marc {get; set;}
        public Guid MarcId {get;set;}
        public List<ProductGroups> Groups { get; set; } = new List<ProductGroups>();
    }
}
