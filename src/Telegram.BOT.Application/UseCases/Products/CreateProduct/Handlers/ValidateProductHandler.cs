using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Products.CreateProduct.Handlers;

public class ValidateProductHandler : Handler<CreateProductRequest>
{
    private readonly INotificationService _notificationService;
    public ValidateProductHandler(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }
    public override async Task ProcessRequest(CreateProductRequest request)
    {
        request.AddLog(LogType.Process, "Executing ValidateProductHandler");
        if (!request.Product.IsValid)
        {
            _notificationService.AddNotifications(request.Product.ValidationResult);
            return;
        }
        if (sucessor != null)
        {
            await sucessor.ProcessRequest(request);
        }
    }
}
