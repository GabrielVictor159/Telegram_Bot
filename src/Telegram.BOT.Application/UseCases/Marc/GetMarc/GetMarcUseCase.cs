using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Boundaries;
using Telegram.BOT.Application.Boundaries.Marc;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Marc.GetMarc.Handlers;
using Telegram.BOT.Application.UseCases.Products.GetProduct.Handlers;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Marc.GetMarc;

public class GetMarcUseCase : IGetMarcRequest
{
    private readonly GetMarcHandler getMarcHandler;
    private readonly ILogRepository logRepository;
    public GetMarcUseCase(GetMarcHandler getMarcHandler, ILogRepository logRepository)
    {
        this.getMarcHandler = getMarcHandler;
        this.logRepository = logRepository;
    }

    public async Task Execute(GetMarcRequest request)
    {
        try
        {
            await getMarcHandler.ProcessRequest(request);
            request.output = new GetMarcOutput() { Marcs = request.Marcs};
        }
        catch (Exception ex)
        {
            request.AddLog(LogType.Error, $"Occurring an error: {ex.Message ?? ex.InnerException?.Message}, stacktrace: {ex.StackTrace}");
            request.IsError = true;
            request.ErrorMessage = ex.Message??"";
        }
        finally
        {
            logRepository.AddRange(request.Logs);
        }
    }
}
