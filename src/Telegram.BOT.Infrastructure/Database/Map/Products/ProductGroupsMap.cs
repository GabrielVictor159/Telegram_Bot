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
            builder.HasOne(pg => pg.Product75)
                .WithMany(p => p.Group75)
                .HasForeignKey(pg => pg.Product75Id);
            builder.HasOne(pg => pg.Product50)
                .WithMany(p => p.Group50)
                .HasForeignKey(pg => pg.Product50Id);
            builder.HasOne(pg => pg.Product25)
                .WithMany(p => p.Group25)
                .HasForeignKey(pg => pg.Product25Id);
            builder.HasOne(pg => pg.Group)
                .WithMany()
                .HasForeignKey(pg => pg.GroupId);
        }
    }
}
