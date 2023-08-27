using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.BOT.Infrastructure.Database.Map;
using Microsoft.EntityFrameworkCore;
using Telegram.BOT.Infrastructure.Database.Entities;

namespace Telegram.BOT.Infrastructure.Database
{
    public class Context : DbContext
    {
        public DbSet<Order> Orders => Set<Order>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Environment.GetEnvironmentVariable("DBCONN");
            var inMemory = Environment.GetEnvironmentVariable("USEINMEMORY");
            if (connectionString != null && inMemory == null)
            {
                optionsBuilder.UseNpgsql(connectionString, options =>
                {
                    options.MigrationsHistoryTable("_MigrationHistory", "Ecommerce");
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
            modelBuilder.ApplyConfiguration(new OrderMap());
            base.OnModelCreating(modelBuilder);
        }


    }
}