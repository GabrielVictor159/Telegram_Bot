using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;

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
        productRepository.Add(request.Product);
        if (sucessor != null)
        {
            await sucessor.ProcessRequest(request);
        }
    }
}
