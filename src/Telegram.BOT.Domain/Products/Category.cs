using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Validator.Products;

namespace Telegram.BOT.Domain.Products
{
    public class Category: Entity
    {
        public Guid Id {get; private set;}
        public string Name {get; private set;} = "";
        public List<Marc> marcs {get; set;} = new();
        public Category(Guid Id, string Name)
        {
            this.Id=Id;
            this.Name=Name;
            Validate(this, new CategoryValidator());
        }
    }
}