using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Telegram.BOT.Infrastructure.Database.Entities.Products;

namespace Telegram.BOT.Infrastructure.Database.Map.Products
{
    public class MarcMap : IEntityTypeConfiguration<Marc>
    {
        public void Configure(EntityTypeBuilder<Marc> builder)
        {
            builder.ToTable("Marc", "Products");
            builder.HasKey(p => p.Id);
            builder.HasOne(P=>P.Category)
                .WithMany(P=>P.marcs)
                .HasForeignKey(p=>p.IdCategory);
            builder.HasMany(p=>p.products)
                .WithOne()
                .HasForeignKey(p=>p.IdMarc);
        }
    }
}