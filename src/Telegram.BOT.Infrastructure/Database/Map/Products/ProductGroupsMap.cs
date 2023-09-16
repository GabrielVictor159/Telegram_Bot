using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Infrastructure.Database.Entities.Products;
using System.Reflection.Emit;

namespace Telegram.BOT.Infrastructure.Database.Map.Products
{
    public class ProductGroupsMap : IEntityTypeConfiguration<ProductGroups>
    {
        public void Configure(EntityTypeBuilder<ProductGroups> builder)
        {
            builder.ToTable("ProductGroups", "Products");
            builder.HasKey(pg => pg.Id);
        }
    }
}
