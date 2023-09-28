using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Validator.Products;

namespace Telegram.BOT.Domain.Products;

public class ProductGroups : Entity<ProductGroups, ProductGroupsValidator>
{
    public Guid Id {get;set;}
    public required Guid ProductId { get; init; }
    public Product? Product { get; set; }
    public required Guid GroupId { get; init; }
    public Groups? Group { get; set; }
    public required double Percentage { get; init; }
    public ProductGroups()
        : base(new ProductGroupsValidator())
    {
    }
}
