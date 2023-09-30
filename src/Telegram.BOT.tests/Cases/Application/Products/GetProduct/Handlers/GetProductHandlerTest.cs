using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Products.GetProduct;
using Telegram.BOT.Application.UseCases.Products.GetProduct.Handlers;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Application.Products.GetProduct.Handlers;
[UseAutofacTestFramework]
public class GetProductHandlerTest
{
    private readonly GetProductsHandler getProductHandler;
    private readonly IProductRepository productRepository;

    public GetProductHandlerTest
        (GetProductsHandler getProductHandler, 
        IProductRepository productRepository)
    {
        this.getProductHandler = getProductHandler;
        this.productRepository = productRepository;
    }
    [Fact]
    public async Task Should()
    {
        var product = ProductBuilder.New().Build();
        productRepository.Add(product);
        var request = new GetProductRequest() { Name = product.Name};
        await getProductHandler.ProcessRequest(request);
        request.Products.Should().NotBeNullOrEmpty();
    }
}
