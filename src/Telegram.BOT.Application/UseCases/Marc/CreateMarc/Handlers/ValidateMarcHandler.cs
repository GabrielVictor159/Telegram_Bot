using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Application.UseCases.Marc.CreateMarc;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Marc.CreateMarc.Handlers;

public class ValidateMarcHandler : Handler<CreateMarcRequest>
{
    private readonly INotificationService notificationService;
    public ValidateMarcHandler(INotificationService notificationService)
    {
        this.notificationService = notificationService;
    }
    public override async Task ProcessRequest(CreateMarcRequest request)
    {
        request.AddLog(LogType.Process, "Executing ValidateMarcHandler");
        if (!request.marc.IsValid)
        {
            notificationService.AddNotifications(request.marc.ValidationResult);
            return;
        }
        if (sucessor != null)
        {
            await sucessor.ProcessRequest(request);
        }
    }
}
