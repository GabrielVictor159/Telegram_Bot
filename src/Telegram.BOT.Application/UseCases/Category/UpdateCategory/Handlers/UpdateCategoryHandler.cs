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
    public class UpdateCategoryHandler : Handler<UpdateCategoryRequest>
    {
        private readonly INotificationService notificationService;
        private readonly ICategoryRepository categoryRepository;

        public UpdateCategoryHandler
            (INotificationService notificationService, 
            ICategoryRepository categoryRepository)
        {
            this.notificationService = notificationService;
            this.categoryRepository = categoryRepository;
        }

        public override async Task ProcessRequest(UpdateCategoryRequest request)
        {
            request.AddLog(LogType.Process, "Executing UpdateCategoryHandler");
            int result = categoryRepository.Update(request.Category);
            if(result ==0)
            {
                request.IsError = true;
                request.ErrorMessage = "For some unknown reason the category was not updated";
                return;
            }
            if (sucessor!=null)
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}
