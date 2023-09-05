using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.BOT.Infrastructure.Database.Map;
using Microsoft.EntityFrameworkCore;
using Telegram.BOT.Infrastructure.Database.Entities;
using Telegram.BOT.Infrastructure.Database.Map.Products;
using Telegram.BOT.Infrastructure.Database.Entities.Products;

namespace Telegram.BOT.Infrastructure.Database
{
    public class Context : DbContext
    {
        public DbSet<Groups> Groups => Set<Groups>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductGroups> ProductGroups => Set<ProductGroups>();
        public DbSet<Marc> Marcs => Set<Marc>();
        public DbSet<Category> Categories => Set<Category>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Environment.GetEnvironmentVariable("DBCONN");
            var inMemory = Environment.GetEnvironmentVariable("USEINMEMORY");
            if (connectionString != null && inMemory == null)
            {
                optionsBuilder.UseNpgsql(connectionString, options =>
                {
                    options.MigrationsHistoryTable("_MigrationHistory", "public");
                    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                });
            }
            else
            {
                optionsBuilder.UseInMemoryDatabase("EcommerceInMemory");
            }
            base.OnConfiguring(optionsBuilder);

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GroupsMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new ProductGroupsMap());
            modelBuilder.ApplyConfiguration(new MarcMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            base.OnModelCreating(modelBuilder);
        }


    }
}