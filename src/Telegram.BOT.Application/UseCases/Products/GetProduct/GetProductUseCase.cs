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
using Telegram.BOT.Application.UseCases.Products.GetProduct.Handlers;

namespace Telegram.BOT.Application.UseCases.Products.GetProduct;

public class GetProductUseCase : IGetProductRequest
{
    private readonly GetProductsHandler getProductsHandler;
    private readonly ILogRepository logRepository;
    public GetProductUseCase(GetProductsHandler getProductsHandler, ILogRepository logRepository)
    {
        this.getProductsHandler = getProductsHandler;
        this.logRepository = logRepository;
    }

    public async Task Execute(GetProductRequest request)
    {
        try
        {
            await getProductsHandler.ProcessRequest(request);
            var output = new List<ProductOutput>();
            request.Products.ForEach(p => output.Add(new ProductOutput() { product= p}));
            request.output = output;
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
