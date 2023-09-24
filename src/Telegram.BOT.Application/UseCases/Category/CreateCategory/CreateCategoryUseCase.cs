using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Boundaries.Products;
using Telegram.BOT.Application.Boundaries;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Products.CreateProduct.Handlers;
using Telegram.BOT.Domain.Enums;
using Telegram.BOT.Application.UseCases.Category.CreateCategory.Handlers;
using Telegram.BOT.Application.Boundaries.Category;

namespace Telegram.BOT.Application.UseCases.Category.CreateCategory;

public class CreateCategoryUseCase : ICreateCategoryRequest
{
    private readonly ValidateCategoryHandler validateCategoryHandler;
    private readonly ILogRepository logRepository;
    private readonly IOutputPort<SaveCategoryOutput> outputPort;
    public CreateCategoryUseCase
        (ValidateCategoryHandler validateCategoryHandler, 
        SaveCategoryHandler saveCategoryHandler,
        ILogRepository logRepository, 
        IOutputPort<SaveCategoryOutput> outputPort)
    {
        this.validateCategoryHandler = validateCategoryHandler.SetSucessor(saveCategoryHandler);
        this.logRepository = logRepository;
        this.outputPort = outputPort;
    }

    public async Task Execute(CreateCategoryRequest request)
    {
        try
        {
            await validateCategoryHandler.ProcessRequest(request);
            outputPort.Standard(new SaveCategoryOutput() { Category = request.category});
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
