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
        public List<Marc> marcs {get; private set;} = new();
        public Category(Guid Id, string Name, List<Marc>? marcs =null)
        {
            this.Id=Id;
            this.Name=Name;
            this.marcs = marcs ?? new();
            Validate(this, new CategoryValidator());
        }
    }
}