using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Boundaries;
using Telegram.BOT.Application.Boundaries.Products;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Products.CreateProduct.Handlers;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Products.CreateProduct;

public class CreateProductUseCase : ICreateProductRequest
{
    private readonly ValidateProductHandler validateProductHandler;
    private readonly ILogRepository logRepository;
    private readonly IOutputPort<ProductOutput> outputPort;
    public CreateProductUseCase
        (ILogRepository logRepository,
        IOutputPort<ProductOutput> outputPort,
        ValidateProductHandler validateProductHandler,
        SaveImageHandler saveImageHandler,
        CreateGroupsHandler createGroupsHandler,
        CreateProductHandler createProduct,
        CreateProductGroupsHandler createProductGroupsHandler)
    {
        this.validateProductHandler = validateProductHandler.SetSucessor
        (
            saveImageHandler.SetSucessor
            (
                createGroupsHandler.SetSucessor
                (
                    createProduct.SetSucessor
                    (
                        createProductGroupsHandler
                    )
                )
            )
        ); 
        this.logRepository = logRepository;
        this.outputPort = outputPort;
    }

    public async Task Execute(CreateProductRequest request)
    {
        try
        {
            await validateProductHandler.ProcessRequest(request);
            outputPort.Standard(new ProductOutput() { product = request.Product});
        }
        catch(Exception ex)
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
