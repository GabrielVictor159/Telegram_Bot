using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Boundaries.Ambient.EnvVariables;
using Telegram.BOT.Application.Boundaries.Products;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.GetEnvByFilter.Handlers;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Ambient.EnvVariables.GetEnvByFilter;

public class GetEnvByFilterUseCase : IGetEnvByFilterRequest
{
    private readonly ILogRepository logRepository;
    private GetEnvVariablesHandler getEnvVariablesHandler;
    public GetEnvByFilterUseCase
        (ILogRepository logRepository,
        GetEnvVariablesHandler getEnvVariablesHandler)
    {
        this.getEnvVariablesHandler = getEnvVariablesHandler;
        this.logRepository = logRepository;
    }
    public async Task Execute(GetEnvByFilterRequest request)
    {
        try
        {
            await getEnvVariablesHandler.ProcessRequest(request);
            var output = new List<GetEnvOutput>();
            request.variablesFound.ForEach(p => output.Add(new GetEnvOutput() { Key = p.Key, Value = p.Value}));
            request.output = output;
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
