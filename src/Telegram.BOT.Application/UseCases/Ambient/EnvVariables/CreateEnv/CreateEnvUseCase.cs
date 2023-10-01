using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Boundaries.Marc;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.CreateEnv.Handlers;
using Telegram.BOT.Application.UseCases.Marc.DeleteMarc.Handlers;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Ambient.EnvVariables.CreateEnv;

public class CreateEnvUseCase : ICreateEnvRequest
{
    private readonly ILogRepository logRepository;
    private ValidateEnvVariableHandler validateEnvVariableHandler;
    public CreateEnvUseCase
        (ILogRepository logRepository,
        ValidateEnvVariableHandler validateEnvVariableHandler,
        GetEnvVariableHandler getEnvVariableHandler,
        SaveEnvVariableHandler saveEnvVariableHandler)
    {
        validateEnvVariableHandler.SetSucessor(
            getEnvVariableHandler.SetSucessor(
                saveEnvVariableHandler));
        this.validateEnvVariableHandler = validateEnvVariableHandler;
        this.logRepository = logRepository;
    }

    public async Task Execute(CreateEnvRequest request)
    {
        try
        {
            await validateEnvVariableHandler.ProcessRequest(request);
            request.output = "Sucess Register Enviorment";
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
