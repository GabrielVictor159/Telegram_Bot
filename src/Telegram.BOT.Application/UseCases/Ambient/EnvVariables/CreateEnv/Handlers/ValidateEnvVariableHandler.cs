using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Ambient.EnvVariables.CreateEnv.Handlers;

public class ValidateEnvVariableHandler : Handler<CreateEnvRequest>
{
    private readonly INotificationService notificationService;
    public ValidateEnvVariableHandler(INotificationService notificationService)
    {
        this.notificationService = notificationService;
    }

    public override async Task ProcessRequest(CreateEnvRequest request)
    {
        request.AddLog(LogType.Process, "Executing ValidateEnvVariableHandler");
        if(!request.EnvVariable.IsValid)
        {
            notificationService.AddNotifications(request.EnvVariable.ValidationResult);
            return;
        }
        if(sucessor!=null)
        {
            await sucessor.ProcessRequest(request); 
        }
    }
}
