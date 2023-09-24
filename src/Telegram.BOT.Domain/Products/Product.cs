using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Validator.Products;

namespace Telegram.BOT.Domain.Products
{
    public class Product : Entity<Product, ProductValidator>
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public required string Description { get; init; }
        public required string Image { get; init; }
        public required string Tags { get; init; }
        public required DateTime CreateDate { get; init; }
        public double Price { get; set; } = 0;
        public required Guid MarcId {get; init;}
        public Marc? Marc {get; set;}
        public List<ProductGroups> Group75 { get; set; } = new();
        public List<ProductGroups> Group50 { get; set; } = new();
        public List<ProductGroups> Group25 { get; set; } = new();

        public Product()
            : base(new ProductValidator())
        {
        }
    }
}
