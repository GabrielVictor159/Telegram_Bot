using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Boundaries.Category;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Category.UpdateCategory.Handlers;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Category.UpdateCategory
{
    public class UpdateCategoryUseCase : IUpdateCategoryRequest
    {
        private readonly ILogRepository logRepository;
        private readonly GetCategoryHandler getCategoryHandler;

        public UpdateCategoryUseCase
            (ILogRepository logRepository, 
            GetCategoryHandler getCategoryHandler,
            VerifyNameDisponibilityHandler verifyNameDisponibilityHandler,
            UpdateCategoryHandler updateCategoryHandler)
        {
            this.logRepository = logRepository;
            getCategoryHandler.SetSucessor
                (verifyNameDisponibilityHandler.SetSucessor
                (updateCategoryHandler));
            this.getCategoryHandler = getCategoryHandler;
        }

        public async Task Execute(UpdateCategoryRequest request)
        {
            try
            {
                await getCategoryHandler.ProcessRequest(request);
                request.output = new UpdateCategoryOutput() { Category = request.Category };
            }
            catch (Exception ex)
            {
                request.AddLog(LogType.Error, $"Occurring an error: {ex.Message ?? ex.InnerException?.Message}, stacktrace: {ex.StackTrace}");
                request.IsError = true;
                request.ErrorMessage = ex.Message ?? "";
            }
            finally
            {
                logRepository.AddRange(request.Logs);
            }
        }

      
    }
}
