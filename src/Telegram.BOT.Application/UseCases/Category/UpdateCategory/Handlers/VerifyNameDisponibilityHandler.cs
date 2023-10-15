using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Application.UseCases.Category.CreateCategory;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Category.UpdateCategory.Handlers
{
    public class VerifyNameDisponibilityHandler : Handler<UpdateCategoryRequest>
    {
        private readonly INotificationService notificationService;
        private readonly ICategoryRepository categoryRepository;
        private readonly IMarcRepository marcRepository;

        public VerifyNameDisponibilityHandler
            (INotificationService notificationService, 
            ICategoryRepository categoryRepository, 
            IMarcRepository marcRepository)
        {
            this.notificationService = notificationService;
            this.categoryRepository = categoryRepository;
            this.marcRepository = marcRepository;
        }

        public override async Task ProcessRequest(UpdateCategoryRequest request)
        {
            request.AddLog(LogType.Process, "Executing VerifyNameDisponibilityHandler");
            var marcs = marcRepository.GetByFilter(e => e.Name.Equals(request.Category.Name), 1, 10);
            var categories = categoryRepository.GetByFilter(e => e.Name.Equals(request.Category.Name ) && e.Id != request.Category.Id, 1, 10);
            if (marcs.Any())
            {
                notificationService.AddNotification("Name Using", "There is already a brand with the same name");
                return;
            }
            if (categories.Any())
            {
                notificationService.AddNotification("Name Using", "A category with the same name already exists");
                return;
            }
            if (sucessor != null)
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}
