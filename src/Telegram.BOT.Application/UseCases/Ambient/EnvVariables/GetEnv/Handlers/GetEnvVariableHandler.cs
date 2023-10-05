using ManagementServices.variables.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Ambient.EnvVariables.GetEnv.Handlers;

public class GetEnvVariableHandler : Handler<GetEnvRequest>
{
    private readonly INotificationService notificationService;
    private readonly IEnvVariableRepository envVariableRepository;

    public GetEnvVariableHandler(INotificationService notificationService, IEnvVariableRepository envVariableRepository)
    {
        this.notificationService = notificationService;
        this.envVariableRepository = envVariableRepository;
    }

    public override async Task ProcessRequest(GetEnvRequest request)
    {
        request.AddLog(LogType.Process, "Executing GetEnvVariableHandler");
        var result = envVariableRepository.Get(request.Key);
        if (result == null) 
        {
            notificationService.AddNotification("Invalid Key", "No variable was found with this key");
            return;
        }
        request.EnvVariable = result;
        if (sucessor != null)
        {
            await sucessor.ProcessRequest(request);
        }
    }
}
