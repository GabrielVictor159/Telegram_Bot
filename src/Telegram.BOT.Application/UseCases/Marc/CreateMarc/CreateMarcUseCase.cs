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
using Telegram.BOT.Application.UseCases.Category.CreateCategory.Handlers;
using Telegram.BOT.Application.Boundaries.Category;
using Telegram.BOT.Application.UseCases.Marc.CreateMarc.Handlers;
using Telegram.BOT.Application.Boundaries.Marc;

namespace Telegram.BOT.Application.UseCases.Marc.CreateMarc;

internal class CreateMarcUseCase : ICreateMarcRequest
{
    private readonly ValidateMarcHandler validateMarcHandler;
    private readonly ILogRepository logRepository;
    private readonly IOutputPort<SaveMarcOutput> outputPort;
    public CreateMarcUseCase
        (ValidateMarcHandler validateMarcHandler, 
        SaveMarcHandler saveMarcHandler,
        ILogRepository logRepository, 
        IOutputPort<SaveMarcOutput> outputPort)
    {
        this.validateMarcHandler = validateMarcHandler.SetSucessor(saveMarcHandler);
        this.logRepository = logRepository;
        this.outputPort = outputPort;
    }

    public async Task Execute(CreateMarcRequest request)
    {
        try
        {
            await validateMarcHandler.ProcessRequest(request);
            outputPort.Standard(new SaveMarcOutput() { Marc = request.marc});
        }
        catch (Exception ex)
        {
            request.AddLog(LogType.Error, $"Occurring an error: {ex.Message ?? ex.InnerException?.Message}, stacktrace: {ex.StackTrace}");
            outputPort.Error(ex.Message ?? "");
        }
        finally
        {
            logRepository.AddRange(request.Logs);
        }
    }
}
