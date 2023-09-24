using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Boundaries.Category;
using Telegram.BOT.Application.Boundaries;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Category.CreateCategory.Handlers;
using Telegram.BOT.Domain.Enums;
using Telegram.BOT.Application.UseCases.Category.DeleteCategory.Handlers;
using Telegram.BOT.Application.Boundaries.Marc;

namespace Telegram.BOT.Application.UseCases.Category.DeleteCategory;

public class DeleteCategoryUseCase : IDeleteCategoryRequest
{
    private readonly DeleteCategoryHandler deleteCategoryHandler;
    private readonly ILogRepository logRepository;
    public DeleteCategoryUseCase
        (DeleteCategoryHandler deleteCategoryHandler, 
        ILogRepository logRepository)
    {
        this.deleteCategoryHandler = deleteCategoryHandler;
        this.logRepository = logRepository;
    }

    public async Task Execute(DeleteCategoryRequest request)
    {
        try
        {
            await deleteCategoryHandler.ProcessRequest(request);
            request.output=new DeleteCategoryOutput() { Message = "Sucess delete category"};
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
