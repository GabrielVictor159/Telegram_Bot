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
    public class UpdateMarcHandler : Handler<UpdateMarcRequest>
    {
        private readonly INotificationService notificationService;
        private readonly IMarcRepository MarcRepository;

        public UpdateMarcHandler
            (INotificationService notificationService, 
            IMarcRepository MarcRepository)
        {
            this.notificationService = notificationService;
            this.MarcRepository = MarcRepository;
        }

        public override async Task ProcessRequest(UpdateMarcRequest request)
        {
            request.AddLog(LogType.Process, "Executing UpdateMarcHandler");
            int result = MarcRepository.Update(request.Marc);
            if(result ==0)
            {
                request.IsError = true;
                request.ErrorMessage = "For some unknown reason the Marc was not updated";
                return;
            }
            if (sucessor!=null)
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}
