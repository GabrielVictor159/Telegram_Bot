using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Validator.Products;

namespace Telegram.BOT.Domain.Products
{
    public class Marc : Entity <Marc, MarcValidator>
    {
        public required Guid Id {get; init;}
        public required string Name {get; init; } = "";
        public Category? Category {get; set;}
        public required Guid CategoryId {get; init; }
        public List<Product> products {get; set;} = new();
        public Marc()
            : base(new MarcValidator())
        {
        }
    }
}