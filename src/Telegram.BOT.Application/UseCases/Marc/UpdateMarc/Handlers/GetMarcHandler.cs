using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Marc.UpdateMarc.Handlers
{
    public class GetMarcHandler : Handler<UpdateMarcRequest>
    {
        private readonly INotificationService notificationService;
        private readonly IMarcRepository MarcRepository;

        public GetMarcHandler
            (INotificationService notificationService, 
            IMarcRepository MarcRepository)
        {
            this.notificationService = notificationService;
            this.MarcRepository = MarcRepository;
        }

        public override async Task ProcessRequest(UpdateMarcRequest request)
        {
            request.AddLog(LogType.Process, "Executing GetMarcHandler");
            var entity = MarcRepository.GetByFilter(e => e.Id == request.Marc.Id,1,10).FirstOrDefault();
            if(entity == null) 
            {
                notificationService.AddNotification("Not Found", $"Unable to find any Marc with the id {request.Marc.Id}");
                return;
            }
           if(sucessor !=null)
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}
