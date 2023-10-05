using Autofac;
using System;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Logs;
using Telegram.BOT.Infrastructure.Modules;
using Telegram.BOT.TelegramJob.Interfaces;
using Telegram.BOT.TelegramJob.Modules;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine(MessageLogs.GabrielSymbol());
        using (var container = BuildContainer())
        {
            var botService = container.Resolve<IBotTelegramService>();

            var botTask = botService.StartAsync(default);

            Console.WriteLine("Pressione Ctrl+C para encerrar o bot.");
            Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                botService.StopAsync(default).GetAwaiter().GetResult(); 
            };

            await botTask; 
        }
    }

    static IContainer BuildContainer()
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule(new ApplicationModule());
        builder.RegisterModule(new InfrastructureModule());
        builder.RegisterModule(new JobModule());
        return builder.Build();
    }
}
