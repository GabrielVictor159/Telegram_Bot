using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Products.CreateProduct.Handlers;

public class CreateProductHandler : Handler<CreateProductRequest>
{
    private readonly IProductRepository productRepository;
    public CreateProductHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public override async Task ProcessRequest(CreateProductRequest request)
    {
        request.AddLog(LogType.Process, "Executing CreateProductHandler");
        productRepository.Add(request.Product);
        if (sucessor != null)
        {
            await sucessor.ProcessRequest(request);
        }
    }
}
