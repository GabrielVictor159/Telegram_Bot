using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Validator.Products;

namespace Telegram.BOT.Domain.Products
{
    public class Groups : Entity<Groups, GroupsValidator>
    {
        public required Guid Id { get; init; }
        public required string Tags { get; init; }
        public required DateTime CreateDate { get; init; } 
        public List<ProductGroups> Group { get; set; } = new List<ProductGroups>();
        public Groups()
            : base(new GroupsValidator())
        {
        }
    }
}
