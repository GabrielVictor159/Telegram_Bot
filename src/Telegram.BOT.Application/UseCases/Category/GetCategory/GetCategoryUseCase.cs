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
    public GetCategoryUseCase(GetCategoryHandler getCategoryHandler, ILogRepository logRepository)
    {
        this.getCategoryHandler = getCategoryHandler;
        this.logRepository = logRepository;
    }

    public async Task Execute(GetCategoryRequest request)
    {
        try
        {
            await getCategoryHandler.ProcessRequest(request);
            request.output=new GetCategoryOutput() { Categories = request.Categories};
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
