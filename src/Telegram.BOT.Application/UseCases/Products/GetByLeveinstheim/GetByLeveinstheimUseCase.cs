using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Boundaries.Products;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Products.GetByLeveinstheim.Handlers;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Products.GetByLeveinstheim
{
    public class GetByLeveinstheimUseCase : IGetByLeveinstheimRequest
    {
        private readonly ILogRepository logRepository;
        private readonly GetGroupsHandler getGroupsHandler;

        public GetByLeveinstheimUseCase
            (ILogRepository logRepository,
            GetGroupsHandler getGroupsHandler,
            GetProductsHandler getProductsHandler)
        {
            this.logRepository = logRepository;
            getGroupsHandler.SetSucessor(getProductsHandler);
            this.getGroupsHandler = getGroupsHandler;
        }

        public async Task Execute(GetByLeveinstheimRequest request)
        {
            try
            {
                await getGroupsHandler.ProcessRequest(request);
                request.output = new() { Products = request.Products };
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
