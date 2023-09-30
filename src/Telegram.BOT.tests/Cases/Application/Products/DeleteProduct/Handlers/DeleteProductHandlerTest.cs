using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Application.UseCases.Products.DeleteProduct;
using Telegram.BOT.Application.UseCases.Products.DeleteProduct.Handlers;
using Telegram.BOT.Domain.Logs;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Application.Products.DeleteProduct.Handlers;
[UseAutofacTestFramework]
public class DeleteProductHandlerTest
{
    private readonly INotificationService notificationService;
    private readonly IProductRepository productRepository;
    private readonly DeleteProductHandler deleteProductHandler;

    public DeleteProductHandlerTest
        (INotificationService notificationService, 
        IProductRepository productRepository,
        DeleteProductHandler deleteProductHandler)
    {
        this.notificationService = notificationService;
        this.productRepository = productRepository;
        this.deleteProductHandler = deleteProductHandler;
    }
    [Fact]
    public async Task ShouldSucess()
    {
        var Image = System.Text.Encoding.UTF8.GetBytes(MessageLogs.GabrielSymbol());
        var product = ProductBuilder.New().Build();
        productRepository.Add(product);
        var request = new DeleteProductRequest() { Product = product, Id = product.Id };
        await deleteProductHandler.ProcessRequest(request);
        notificationService.HasNotifications.Should().BeFalse();
    }
    [Fact]
    public async Task ShouldFailureByNotFound()
    {
        var Image = System.Text.Encoding.UTF8.GetBytes(MessageLogs.GabrielSymbol());
        var product = ProductBuilder.New().Build();
        var request = new DeleteProductRequest() { Product = product, Id = product.Id };
        await deleteProductHandler.ProcessRequest(request);
        notificationService.HasNotifications.Should().BeTrue();
    }
}
