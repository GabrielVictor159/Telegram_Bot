using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.BOT.Infrastructure.Database.Map.Log
{
    public class LogMap : IEntityTypeConfiguration<Entities.Logs.Log>
    {
        public void Configure(EntityTypeBuilder<Entities.Logs.Log> builder)
        {
            builder.ToTable("Logs", "public");
            builder.HasKey(e => e.Id);
        }

    }
}
