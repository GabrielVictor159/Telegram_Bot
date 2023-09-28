using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Products.CreateProduct;
using Telegram.BOT.Application.UseCases.Products.CreateProduct.Handlers;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Application.Products.CreateProduct.Handlers;
[UseAutofacTestFramework]
public class CreateProductHandlerTest
{
    private readonly CreateProductHandler createProductHandler;
    private readonly IProductRepository productRepository;
    public CreateProductHandlerTest
        (CreateProductHandler createProductHandler,
        IProductRepository productRepository)
    {
        this.createProductHandler = createProductHandler;
        this.productRepository = productRepository;
    }
    [Fact]
    public async Task ShouldSucess()
    {
        var request = new CreateProductRequest() { Product = ProductBuilder.New().Build(), Image = new byte[10] };
        await createProductHandler.ProcessRequest(request);
        productRepository.GetByFilter(e=>e.Id == request.Product.Id,1,10).FirstOrDefault().Should().NotBeNull();
    }
}
