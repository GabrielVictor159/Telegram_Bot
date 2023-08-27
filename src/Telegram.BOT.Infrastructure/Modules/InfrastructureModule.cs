using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Telegram.BOT.Infrastructure.Database;

namespace Telegram.BOT.Infrastructure.Modules
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(InfrastructureException).Assembly)
            .AsImplementedInterfaces().AsSelf().InstancePerLifetimeScope();

            Mapper(builder);
            DataAccess(builder);
            base.Load(builder);
        }
         private void DataAccess(ContainerBuilder builder)
        {
            var connection = Environment.GetEnvironmentVariable("DBCONN");

            builder.RegisterAssemblyTypes(typeof(InfrastructureException).Assembly)
                .Where(t => (t.Namespace ?? string.Empty).Contains("Database"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            
            if (!string.IsNullOrEmpty(connection))
            {
                using var context = new Context();                
                context.Database.Migrate();               
            }

        }
        private void Mapper(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(InfrastructureException).Assembly)
              .Where(t => (t.Namespace ?? string.Empty).Contains("Database") && typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract && t.IsPublic)
              .As<Profile>();
            builder.Register(c => new MapperConfiguration(cfg =>
            {
                foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
                cfg.AddExpressionMapping();
            })).AsSelf().SingleInstance();
            builder.Register(c => c.Resolve<MapperConfiguration>()
                .CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}