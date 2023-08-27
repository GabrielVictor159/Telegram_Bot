using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.BOT.Infrastructure.Database.Entities.Products
{
    public class ProductGroups
    {
        public Guid Id {get;set;}
        public Guid Product75Id { get; set; }
        public Product? Product75 { get; set; }
        public Guid Product50Id { get; set; }
        public Product? Product50 { get; set; }
        public Guid Product25Id { get; set; }
        public Product? Product25 { get; set; }
        public Guid GroupId { get; set; }
        public required Groups Group { get; set; }
    }
}
