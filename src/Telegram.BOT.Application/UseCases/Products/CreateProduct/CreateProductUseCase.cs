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
    public CreateProductUseCase
        (ILogRepository logRepository,
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
    }

    public async Task Execute(CreateProductRequest request)
    {
        try
        {
            await validateProductHandler.ProcessRequest(request);
            request.output=new ProductOutput() { product = request.Product};
        }
        catch(Exception ex)
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
