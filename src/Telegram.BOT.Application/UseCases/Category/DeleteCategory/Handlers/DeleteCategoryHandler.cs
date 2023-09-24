using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Category.DeleteCategory.Handlers;

public class DeleteCategoryHandler : Handler<DeleteCategoryRequest>
{
    private readonly ICategoryRepository categoryRepository;
    private readonly INotificationService notificationService;
    public DeleteCategoryHandler(ICategoryRepository categoryRepository, INotificationService notificationService)
    {
        this.categoryRepository = categoryRepository;
        this.notificationService = notificationService;
    }

    public override async Task ProcessRequest(DeleteCategoryRequest request)
    {
        request.AddLog(LogType.Process, "Executing DeleteCategoryHandler");
        var sucess = categoryRepository.Delete(request.Id);
        if(!sucess)
        {
            notificationService.AddNotification("Invalid Id", "There is no Category with this Past Id");
        }
        if (sucessor != null)
        {
            await sucessor.ProcessRequest(request);
        }
    }
}
