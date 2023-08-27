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
        public List<ProductGroups> Group75 { get; set; } = new List<ProductGroups>();
        public List<ProductGroups> Group50 { get; set; } = new List<ProductGroups>();
        public List<ProductGroups> Group25 { get; set; } = new List<ProductGroups>();
    }
}
