using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Category.UpdateCategory.Handlers
{
    public class GetCategoryHandler : Handler<UpdateCategoryRequest>
    {
        private readonly INotificationService notificationService;
        private readonly ICategoryRepository categoryRepository;

        public GetCategoryHandler
            (INotificationService notificationService, 
            ICategoryRepository categoryRepository)
        {
            this.notificationService = notificationService;
            this.categoryRepository = categoryRepository;
        }

        public override async Task ProcessRequest(UpdateCategoryRequest request)
        {
            request.AddLog(LogType.Process, "Executing GetCategoryHandler");
            var entity = categoryRepository.GetByFilter(e => e.Id == request.Category.Id,1,10).FirstOrDefault();
            if(entity == null) 
            {
                notificationService.AddNotification("Not Found", $"Unable to find any category with the id {request.Category.Id}");
                return;
            }
           if(sucessor !=null)
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}
