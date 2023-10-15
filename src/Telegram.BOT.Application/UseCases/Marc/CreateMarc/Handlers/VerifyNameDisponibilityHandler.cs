using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Application.UseCases.Marc.CreateMarc;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Marc.CreateMarc.Handlers
{
    public class VerifyNameDisponibilityHandler : Handler<CreateMarcRequest>
    {
        private readonly INotificationService notificationService;
        private readonly ICategoryRepository categoryRepository;
        private readonly IMarcRepository marcRepository;

        public VerifyNameDisponibilityHandler(INotificationService notificationService, ICategoryRepository categoryRepository, IMarcRepository marcRepository)
        {
            this.notificationService = notificationService;
            this.categoryRepository = categoryRepository;
            this.marcRepository = marcRepository;
        }

        public override async Task ProcessRequest(CreateMarcRequest request)
        {
            request.AddLog(LogType.Process, "Executing VerifyNameDisponibilityHandler");
            var marcs = marcRepository.GetByFilter(e => e.Name.Equals(request.marc.Name),1,10);
            var categories = categoryRepository.GetByFilter(e => e.Name.Equals(request.marc.Name), 1, 10);
            if(marcs.Any()) 
            {
                notificationService.AddNotification("Name Using", "There is already a brand with the same name");
                return;
            }
            if (categories.Any())
            {
                notificationService.AddNotification("Name Using", "A category with the same name already exists");
                return;
            }
            if (sucessor!=null)
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}
