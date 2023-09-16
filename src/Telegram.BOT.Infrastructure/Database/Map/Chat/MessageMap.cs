using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Telegram.BOT.Infrastructure.Database.Entities.Chat;

namespace Telegram.BOT.Infrastructure.Database.Map.Chat
{
    public class MessageMap : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Message", "Chats");
            builder.HasKey(e => e.Id);
            builder.Property(e=>e.Messaging).IsRequired();
            builder.Property(e=>e.ChatId).IsRequired();
        }
        
    }
}