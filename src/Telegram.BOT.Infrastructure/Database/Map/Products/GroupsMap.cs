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
    public class GroupsMap : IEntityTypeConfiguration<Groups>
    {
        public void Configure(EntityTypeBuilder<Groups> builder)
        {
            builder.ToTable("Group", "Products");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Tags).HasMaxLength(1500).IsRequired();
            builder.Property(e => e.CreateDate).IsRequired();
        }
    }
}
