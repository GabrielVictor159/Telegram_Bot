using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Boundaries.Marc;
using Telegram.BOT.Application.Boundaries;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Marc.CreateMarc.Handlers;
using Telegram.BOT.Domain.Enums;
using Telegram.BOT.Application.UseCases.Marc.DeleteMarc.Handlers;

namespace Telegram.BOT.Application.UseCases.Marc.DeleteMarc;

public class DeleteMarcUseCase : IDeleteMarcRequest
{
    private readonly DeleteMarcHandler deleteMarcHandler;
    private readonly ILogRepository logRepository;
    public DeleteMarcUseCase
        (DeleteMarcHandler deleteMarcHandler, 
        ILogRepository logRepository)
    {
        this.deleteMarcHandler = deleteMarcHandler;
        this.logRepository = logRepository;
    }

    public async Task Execute(DeleteMarcRequest request)
    {
        try
        {
            await deleteMarcHandler.ProcessRequest(request);
            request.output=new DeleteMarcOutput() { Message = "Sucess delete Marc"};
        }
        catch (Exception ex)
        {
            request.AddLog(LogType.Error, $"Occurring an error: {ex.Message ?? ex.InnerException?.Message}, stacktrace: {ex.StackTrace}");
            request.IsError = true;
            request.ErrorMessage=ex.Message ?? "";
        }
        finally
        {
            logRepository.AddRange(request.Logs);
        }
    }
}
