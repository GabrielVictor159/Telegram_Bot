using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Application.UseCases.Marc.CreateMarc;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Marc.UpdateMarc.Handlers
{
    public class VerifyNameDisponibilityHandler : Handler<UpdateMarcRequest>
    {
        private readonly INotificationService notificationService;
        private readonly IMarcRepository MarcRepository;
        private readonly IMarcRepository marcRepository;

        public VerifyNameDisponibilityHandler
            (INotificationService notificationService, 
            IMarcRepository MarcRepository, 
            IMarcRepository marcRepository)
        {
            this.notificationService = notificationService;
            this.MarcRepository = MarcRepository;
            this.marcRepository = marcRepository;
        }

        public override async Task ProcessRequest(UpdateMarcRequest request)
        {
            request.AddLog(LogType.Process, "Executing VerifyNameDisponibilityHandler");
            var marcs = marcRepository.GetByFilter(e => e.Name.Equals(request.Marc.Name) && e.Id != request.Marc.Id, 1, 10);
            var categories = MarcRepository.GetByFilter(e => e.Name.Equals(request.Marc.Name ), 1, 10);
            if (marcs.Any())
            {
                notificationService.AddNotification("Name Using", "There is already a brand with the same name");
                return;
            }
            if (categories.Any())
            {
                notificationService.AddNotification("Name Using", "Category with the same already exists");
                return;
            }
            if (sucessor != null)
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}
