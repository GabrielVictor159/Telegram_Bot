using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Products.GetProduct.Handlers;
using Telegram.BOT.Application.UseCases.Products.GetProduct;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;
using FluentAssertions;

namespace Telegram.BOT.tests.Cases.Application.Products.GetProduct;
[UseAutofacTestFramework]
public class GetProductUseCaseTest
{
    private readonly GetProductUseCase getProductUseCase;
    private readonly IProductRepository productRepository;

    public GetProductUseCaseTest
        (GetProductUseCase getProductUseCase,
        IProductRepository productRepository)
    {
        this.getProductUseCase = getProductUseCase;
        this.productRepository = productRepository;
    }
    [Fact]
    public async Task ShouldSucess()
    {
        var product = ProductBuilder.New().Build();
        productRepository.Add(product);
        var request = new GetProductRequest() { Name = product.Name };
        await getProductUseCase.Execute(request);
        request.IsError.Should().BeFalse();
        request.output.Should().NotBeNullOrEmpty();
    }
}
