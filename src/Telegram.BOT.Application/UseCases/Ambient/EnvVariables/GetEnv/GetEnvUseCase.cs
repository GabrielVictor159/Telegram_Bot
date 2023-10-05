using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.GetEnv.Handlers;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Ambient.EnvVariables.GetEnv;

public class GetEnvUseCase : IGetEnvRequest
{
    private readonly ILogRepository logRepository;
    private GetEnvVariableHandler getEnvVariableHandler;
    public GetEnvUseCase
        (ILogRepository logRepository,
        GetEnvVariableHandler getEnvVariableHandler)
    {
        this.getEnvVariableHandler = getEnvVariableHandler;
        this.logRepository = logRepository;
    }
    public async Task Execute(GetEnvRequest request)
    {
        try
        {
            await getEnvVariableHandler.ProcessRequest(request);
            request.output = new() { Key = request.EnvVariable!.Key, Value = request.EnvVariable.Value };
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
