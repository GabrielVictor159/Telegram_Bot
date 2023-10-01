using ManagementServices.variables.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Ambient.EnvVariables.CreateEnv.Handlers
{
    public class SaveEnvVariableHandler : Handler<CreateEnvRequest>
    {
        private readonly IEnvVariableRepository envVariableRepository;
        private readonly INotificationService notificationService;
        public SaveEnvVariableHandler
            (IEnvVariableRepository envVariableRepository,
            INotificationService notificationService)
        {
            this.envVariableRepository = envVariableRepository;
            this.notificationService = notificationService;
        }
        public override async Task ProcessRequest(CreateEnvRequest request)
        {
            request.AddLog(LogType.Process, "Executing SaveEnvVariableHandler");
            if (envVariableRepository.Add(request.EnvVariable) != 1)
            {
                notificationService.AddNotification("Invalid Save", $"Unable to save entity to database");
                return;
            };
            if (sucessor != null)
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}
