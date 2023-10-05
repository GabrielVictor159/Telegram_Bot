
using ManagementServices.variables.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Ambient.EnvVariables.CreateEnv.Handlers;

public class GetEnvVariableHandler : Handler<CreateEnvRequest>
{
    private readonly IEnvVariableRepository envVariableRepository;
    private readonly INotificationService notificationService;
    public GetEnvVariableHandler
        (IEnvVariableRepository envVariableRepository,
        INotificationService notificationService)
    {
        this.envVariableRepository = envVariableRepository;
        this.notificationService = notificationService;
    }
    public override async Task ProcessRequest(CreateEnvRequest request)
    {
        request.AddLog(LogType.Process, "Executing GetEnvVariableHandler");
        if (envVariableRepository.Get(request.EnvVariable.Key)!=null)
        {
            notificationService.AddNotification("Invalid Save", $"There is already a variable with this key ${request.EnvVariable.Key}");
            return;
        }
        if (sucessor != null)
        {
            await sucessor.ProcessRequest(request);
        }
    }
}
