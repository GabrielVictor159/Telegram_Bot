using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.BOT.Infrastructure.Database.Map;
using Microsoft.EntityFrameworkCore;
using Telegram.BOT.Infrastructure.Database.Entities;
using Telegram.BOT.Infrastructure.Database.Map.Products;
using Telegram.BOT.Infrastructure.Database.Entities.Products;
using Telegram.BOT.Infrastructure.Database.Entities.Chat;
using Telegram.BOT.Infrastructure.Database.Map.Chat;
using Telegram.BOT.Infrastructure.Database.Entities.Logs;
using Telegram.BOT.Infrastructure.Database.Map.Log;
using Telegram.BOT.Infrastructure.Service;

namespace Telegram.BOT.Infrastructure.Database
{
    public class Context : DbContext
    {
        public DbSet<Groups> Groups => Set<Groups>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductGroups> productGroups => Set<ProductGroups>();
        public DbSet<Chat> chats => Set<Chat>();
        public DbSet<Message> messages => Set<Message>();
        public DbSet<Marc> Marcs => Set<Marc>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Log> logs => Set<Log>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (Environment.GetEnvironmentVariable("DBCONN") != null)
                optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("DBCONN"), options =>
                {
                    options.EnableRetryOnFailure(2, TimeSpan.FromSeconds(5), new List<string>());
                    options.MigrationsHistoryTable("_MigrationHistory", "Migrations");
                });
            else
                optionsBuilder.UseInMemoryDatabase("TelegramBotInMemory");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GroupsMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new ProductGroupsMap());
            modelBuilder.ApplyConfiguration(new ChatMap());
            modelBuilder.ApplyConfiguration(new MessageMap());
            modelBuilder.ApplyConfiguration(new MarcMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new LogMap());
            base.OnModelCreating(modelBuilder);
        }


    }
}