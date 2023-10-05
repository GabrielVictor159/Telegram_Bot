using Autofac;
using Telegram.BOT.Domain.Logs;
using Telegram.BOT.Infrastructure.Modules;
using Telegram.BOT.Services.Jobs.ManagementChats;
using Telegram.BOT.Services.Jobs.ManagementGroups;
using Telegram.BOT.Services.Jobs.ManagementImages;
using Telegram.BOT.Services.Jobs.ManagementLogs;
using Telegram.BOT.Services.Modules;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine(MessageLogs.GabrielSymbol());
        using (var container = BuildContainer())
        {
            var removeChatsService = container.Resolve<RemoveChatsAfterDayUseCase>();
            removeChatsService.StartAsync(new CancellationToken());

            var removeGroupsNotUseCase = container.Resolve<RemoveGroupsNotUseCase>();
            removeGroupsNotUseCase.StartAsync(new CancellationToken());

            var removeImagesNotUseCase = container.Resolve<RemoveImagesNotUseCase>();
            removeImagesNotUseCase.StartAsync(new CancellationToken());

            var removeLogsAfterDayUseCase = container.Resolve<RemoveLogsAfterDayUseCase>();
            removeLogsAfterDayUseCase.StartAsync(new CancellationToken());

            while (true)
            {
                await Task.Delay(TimeSpan.FromMinutes(60));
            }
        }
    }

    private static IContainer BuildContainer()
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule(new ApplicationModule());
        builder.RegisterModule(new InfrastructureModule());
        builder.RegisterModule(new ServicesModule());
        return builder.Build();
    }
}