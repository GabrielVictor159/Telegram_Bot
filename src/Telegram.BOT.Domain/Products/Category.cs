using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Validator.Products;

namespace Telegram.BOT.Domain.Products
{
    public class Category: Entity<Category, CategoryValidator>
    {
        public required Guid Id {get; init;}
        public required string Name {get; init;} = "";
        public List<Marc> marcs {get; set;} = new();
        public Category()
            : base (new CategoryValidator())
        {
        }
    }
}