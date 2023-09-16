using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.BOT.Infrastructure.Database.Entities.Products
{
    public class Groups
    {
        public Guid Id { get; set; }
        public string Tags { get; set; } = "";
        public DateTime CreateDate { get; set; }
        public List<ProductGroups> Group { get; set; } = new List<ProductGroups>();
    }
}
