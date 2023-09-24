using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Category.CreateCategory.Handlers;

public class ValidateCategoryHandler : Handler<CreateCategoryRequest>
{
    private readonly INotificationService notificationService;
    public ValidateCategoryHandler(INotificationService notificationService)
    {
        this.notificationService = notificationService;
    }
    public override async Task ProcessRequest(CreateCategoryRequest request)
    {
        request.AddLog(LogType.Process, "Executing ValidateCategoryHandler");
        if (!request.category.IsValid)
        {
            notificationService.AddNotifications(request.category.ValidationResult);
            return;
        }
        if (sucessor != null)
        {
            await sucessor.ProcessRequest(request);
        }
    }
}
