using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Telegram.BOT.Infrastructure.Database.Entities.Products;

namespace Telegram.BOT.Infrastructure.Database.Map.Products
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
        builder.ToTable("Category", "Products");
        builder.HasKey(p => p.Id);
        builder.Property(p=>p.Name).IsRequired();
        builder.HasIndex(p => p.Name).IsUnique();
        }
    }
}