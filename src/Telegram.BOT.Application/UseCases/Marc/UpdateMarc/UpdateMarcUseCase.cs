using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Boundaries.Marc;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Marc.UpdateMarc.Handlers;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Marc.UpdateMarc
{
    public class UpdateMarcUseCase : IUpdateMarcRequest
    {
        private readonly ILogRepository logRepository;
        private readonly GetMarcHandler getMarcHandler;

        public UpdateMarcUseCase
            (ILogRepository logRepository, 
            GetMarcHandler getMarcHandler,
            VerifyNameDisponibilityHandler verifyNameDisponibilityHandler,
            UpdateMarcHandler updateMarcHandler)
        {
            this.logRepository = logRepository;
            getMarcHandler.SetSucessor
                (verifyNameDisponibilityHandler.SetSucessor
                (updateMarcHandler));
            this.getMarcHandler = getMarcHandler;
        }

        public async Task Execute(UpdateMarcRequest request)
        {
            try
            {
                await getMarcHandler.ProcessRequest(request);
                request.output = new UpdateMarcOutput() { Marc = request.Marc };
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
