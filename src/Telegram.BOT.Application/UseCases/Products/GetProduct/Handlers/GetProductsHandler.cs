using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Products.GetProduct.Handlers;

public class GetProductsHandler : Handler<GetProductRequest>
{
    private readonly IProductRepository productRepository;
    public GetProductsHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }
    public override async Task ProcessRequest(GetProductRequest request)
    {
        request.AddLog(LogType.Process, "Executing GetProductsHandler");
        if (request.expression == null)
        {
            request.Products.AddRange(productRepository.GetByFilter(
                (e =>
                e.Name.ToLower().Contains(request.Name.ToLower()) &&
                e.Tags.ToLower().Contains(request.Tag.ToLower()) &&
                e.CreateDate >= request.DeDate &&
                e.CreateDate <= request.AteDate &&
                e.Description.ToLower().Contains(request.Description.ToLower())
                ), request.page, request.pageSize));
        }
        else 
        {
            request.Products.AddRange(productRepository.GetByFilter(
                (request.expression), request.page, request.pageSize));
        }
        if (sucessor != null)
        {
            await sucessor.ProcessRequest(request);
        }
    }
}
