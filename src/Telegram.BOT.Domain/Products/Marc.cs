using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Validator.Products;

namespace Telegram.BOT.Domain.Products
{
    public class Marc : Entity
    {
        public Guid Id {get; private set;}
        public string Name {get; private set;} = "";
        public Category? Category {get; set;}
        public Guid CategoryId {get; private set;}
        public List<Product> products {get; set;} = new();
        public Marc(Guid Id, string Name, Guid CategoryId)
        {
            this.Id = Id;
            this.Name = Name;
            this.products = products ?? new List<Product>();
            this.CategoryId = CategoryId;
            Validate(this, new MarcValidator());
        }
    }
}