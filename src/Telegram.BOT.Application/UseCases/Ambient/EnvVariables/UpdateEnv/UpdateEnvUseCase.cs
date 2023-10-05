using ManagementServices.variables.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Ambient.EnvVariables.UpdateEnv
{
    public class UpdateEnvUseCase : IUpdateEnvRequest
    {
        private readonly ILogRepository logRepository;
        private readonly IEnvVariableRepository envVariableRepository;

        public UpdateEnvUseCase(ILogRepository logRepository, IEnvVariableRepository envVariableRepository)
        {
            this.logRepository = logRepository;
            this.envVariableRepository = envVariableRepository;
        }

        public void Execute(UpdateEnvRequest request)
        {
            try
            {
                request.AddLog(LogType.Process, $"Executing RemoveEnvUseCase");
                var result = envVariableRepository.Update(request.variable);
                if (result == 0)
                {
                    request.IsError = true;
                    request.ErrorMessage = $"Unable to find variable with this key {request.variable.Key}";
                }
                else
                {
                    request.output = "environment variable updated";
                }
            }
            catch (Exception ex)
            {
                request.AddLog(LogType.Error, $"Occurring an error: {ex.Message ?? ex.InnerException?.Message}, stacktrace: {ex.StackTrace}");
                request.IsError = true;
                request.ErrorMessage = ex.Message ?? "";
            }
            finally
            {
                logRepository.AddRange(request.Logs);
            }
        }
    }
}
