using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Boundaries.Products;
using Telegram.BOT.Application.Boundaries;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Products.CreateProduct.Handlers;
using Telegram.BOT.Domain.Enums;
using Telegram.BOT.Application.Boundaries.Category;
using Telegram.BOT.Application.UseCases.Marc.CreateMarc.Handlers;
using Telegram.BOT.Application.Boundaries.Marc;

namespace Telegram.BOT.Application.UseCases.Marc.CreateMarc;

public class CreateMarcUseCase : ICreateMarcRequest
{
    private readonly ValidateMarcHandler validateMarcHandler;
    private readonly ILogRepository logRepository;
    public CreateMarcUseCase
        (ValidateMarcHandler validateMarcHandler,
        VerifyNameDisponibilityHandler verifyNameDisponibilityHandler,
        SaveMarcHandler saveMarcHandler,
        ILogRepository logRepository)
    {
        this.validateMarcHandler = validateMarcHandler.SetSucessor
            (verifyNameDisponibilityHandler.SetSucessor
            (saveMarcHandler));
        this.logRepository = logRepository;
    }

    public async Task Execute(CreateMarcRequest request)
    {
        try
        {
            await validateMarcHandler.ProcessRequest(request);
            request.output = new SaveMarcOutput() { Marc = request.marc};
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
