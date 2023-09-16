using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Validator.Products;

namespace Telegram.BOT.Domain.Products
{
    public class Groups : Entity
    {
        public Guid Id { get; private set; }
        public string Tags { get; private set; }
        public DateTime CreateDate { get; private set; } 
        public List<ProductGroups> Group { get; set; } = new List<ProductGroups>();
        public Groups(string Tags)
        {
            Id = Guid.NewGuid();
            this.Tags = Tags;
            CreateDate = DateTime.Now;
            Validate(this, new GroupsValidator());
        }
        public Groups(Guid Id, string Tags, DateTime CreateDate)
        {
            this.Id = Id;
            this.Tags = Tags;
            this.CreateDate = CreateDate;
            Validate(this, new GroupsValidator());
        }
    }
}
