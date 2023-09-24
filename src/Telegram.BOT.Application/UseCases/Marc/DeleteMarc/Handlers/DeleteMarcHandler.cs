using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Marc.DeleteMarc.Handlers;

public class DeleteMarcHandler : Handler<DeleteMarcRequest>
{
    private readonly IMarcRepository MarcRepository;
    private readonly INotificationService notificationService;
    public DeleteMarcHandler(IMarcRepository MarcRepository, INotificationService notificationService)
    {
        this.MarcRepository = MarcRepository;
        this.notificationService = notificationService;
    }

    public override async Task ProcessRequest(DeleteMarcRequest request)
    {
        request.AddLog(LogType.Process, "Executing DeleteMarcHandler");
        var sucess = MarcRepository.Delete(request.Id);
        if(!sucess)
        {
            notificationService.AddNotification("Invalid Id", "There is no Marc with this Past Id");
        }
        if (sucessor != null)
        {
            await sucessor.ProcessRequest(request);
        }
    }
}
