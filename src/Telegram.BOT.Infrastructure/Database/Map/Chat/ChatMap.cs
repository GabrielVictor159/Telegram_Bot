using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Telegram.BOT.Infrastructure.Database.Map.Chat
{
    public class ChatMap : IEntityTypeConfiguration<Entities.Chat.Chat>
    {
        public void Configure(EntityTypeBuilder<Entities.Chat.Chat> builder)
        {
            builder.ToTable("Chat", "Chats");
            builder.HasKey(e => e.Id);
            builder.Property(e=>e.CreateDateTime).IsRequired();
        }
    }
}