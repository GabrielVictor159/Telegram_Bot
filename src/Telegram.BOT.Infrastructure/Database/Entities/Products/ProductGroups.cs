using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.BOT.Infrastructure.Database.Entities.Products
{
    public class ProductGroups
    {
        public Guid Id { get; set; }
        public required Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public required Guid GroupId { get; set; }
        public Groups? Group { get; set; }
        public required double Percentage { get; set; }
    }
}
