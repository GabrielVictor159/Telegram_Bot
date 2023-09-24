using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Domain.Products;

namespace Telegram.BOT.Application.UseCases.Products.CreateProduct.Handlers;

public class CreateProductGroupsHandler : Handler<CreateProductRequest>
{
    private readonly IProductGroupsRepository productGroupsRepository;
    public CreateProductGroupsHandler(IProductGroupsRepository productGroupsRepository)
    {
        this.productGroupsRepository = productGroupsRepository;
    }

    public override async Task ProcessRequest(CreateProductRequest request)
    {
        var productGroups = new List<ProductGroups>() { new ProductGroups() { Group = request.groups100[0], Product75Id = request.Product.Id} };
        request.groups75.ForEach(e=>productGroups.Add(new ProductGroups() { Group = e, Product75Id = request.Product.Id }));
        request.groups50.ForEach(e => productGroups.Add(new ProductGroups() { Group = e, Product50Id = request.Product.Id }));
        request.groups25.ForEach(e => productGroups.Add(new ProductGroups() { Group = e, Product25Id = request.Product.Id }));
        productGroupsRepository.AddRange(productGroups);
        if (sucessor != null)
        {
            await sucessor.ProcessRequest(request);
        }
    }
}
