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
        public Category? Category {get; private set;}
        public List<Product> products {get; private set;} = new();
        public Marc(Guid Id, string Name, List<Product>? products = null, Category? category = null)
        {
            this.Id = Id;
            this.Name = Name;
            this.products = products ?? new List<Product>();
            this.Category = category;
            Validate(this, new MarcValidator());
        }
    }
}