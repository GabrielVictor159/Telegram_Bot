using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Infrastructure.Database.Entities.Products;

namespace Telegram.BOT.Infrastructure.Database.Map.Products
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product", "Products");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Description).IsRequired(false);
            builder.Property(p => p.Image).IsRequired(false);
            builder.Property(p => p.Tags).IsRequired();
            builder.Property(p => p.CreateDate).IsRequired();
            builder.Property(p => p.Price).IsRequired();
            builder.HasMany(p => p.Group75)
               .WithOne(pg => pg.Product75)
               .HasForeignKey(pg => pg.Product75Id);
            builder.HasMany(p => p.Group50)
              .WithOne(pg => pg.Product50)
              .HasForeignKey(pg => pg.Product50Id);
            builder.HasMany(p => p.Group25)
              .WithOne(pg => pg.Product25)
              .HasForeignKey(pg => pg.Product25Id);
        }
    }
}
