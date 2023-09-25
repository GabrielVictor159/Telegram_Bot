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
using Telegram.BOT.Application.UseCases.Products.DeleteProduct.Handlers;

namespace Telegram.BOT.Application.UseCases.Products.DeleteProduct;

public class DeleteProductUseCase : IDeleteProductRequest
{
    private readonly GetProductHandler getProductHandler;
    private readonly ILogRepository logRepository;
    public DeleteProductUseCase
        (GetProductHandler getProductHandler,
        DeleteImageProductHandler deleteImageProductHandler,
        DeleteProductHandler deleteProductHandler,
        ILogRepository logRepository)
    {

        this.getProductHandler = getProductHandler.SetSucessor
            (deleteImageProductHandler.SetSucessor(deleteProductHandler));
        this.logRepository = logRepository;
    }

    public async Task Execute(DeleteProductRequest request)
    {
        try
        {
            await getProductHandler.ProcessRequest(request);
            request.output=new DeleteProductOutput() { message = "deleted product" };
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
