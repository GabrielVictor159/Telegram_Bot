using ManagementServices.variables.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Boundaries.Ambient.EnvVariables;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.GetEnvByFilter.Handlers;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Ambient.EnvVariables.RemoveEnv
{
    public class RemoveEnvUseCase : IRemoveEnvRequest
    {
        private readonly IEnvVariableRepository envVariableRepository;
        private readonly ILogRepository logRepository;
        public RemoveEnvUseCase
            (IEnvVariableRepository envVariableRepository,
            ILogRepository logRepository)
        {
            this.envVariableRepository = envVariableRepository;
            this.logRepository = logRepository;
        }

        public void Execute(RemoveEnvRequest request)
        {
            try
            {
                request.AddLog(LogType.Process, $"Executing RemoveEnvUseCase");
                var result = envVariableRepository.Delete(request.Key);
                if(result==0)
                {
                    request.IsError = true;
                    request.ErrorMessage = $"Unable to find variable with this key {request.Key}";
                }
                else
                {
                    request.output = "environment variable deleted";
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
