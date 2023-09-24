using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Boundaries;
using Telegram.BOT.Application.Boundaries.Category;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Category.GetCategory.Handlers;
using Telegram.BOT.Application.UseCases.Products.GetProduct.Handlers;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Category.GetCategory;

public class GetCategoryUseCase : IGetCategoryRequest
{
    private readonly GetCategoryHandler getCategoryHandler;
    private readonly ILogRepository logRepository;
    private readonly IOutputPort<GetCategoryOutput> outputPort;
    public GetCategoryUseCase(GetCategoryHandler getCategoryHandler, ILogRepository logRepository, IOutputPort<GetCategoryOutput> outputPort)
    {
        this.getCategoryHandler = getCategoryHandler;
        this.logRepository = logRepository;
        this.outputPort = outputPort;
    }

    public async Task Execute(GetCategoryRequest request)
    {
        try
        {
            await getCategoryHandler.ProcessRequest(request);
            outputPort.Standard(new GetCategoryOutput() { Categories = request.Categories});
        }
        catch (Exception ex)
        {
            request.AddLog(LogType.Error, $"Occurring an error: {ex.Message ?? ex.InnerException?.Message}, stacktrace: {ex.StackTrace}");
            outputPort.Error(ex.Message ?? "");
        }
        finally
        {
            logRepository.AddRange(request.Logs);
        }
    }
}
