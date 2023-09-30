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
using Telegram.BOT.ImagesManagement.Services;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Application.Products.DeleteProduct;
[UseAutofacTestFramework]
public class DeleteProductUseCaseTest
{
    private readonly INotificationService notificationService;
    private readonly IProductRepository productRepository;
    private readonly DeleteProductUseCase deleteProductUseCase;
    private readonly ImagesManagementServices imagesManagementServices;

    public DeleteProductUseCaseTest
        (INotificationService notificationService, 
        IProductRepository productRepository, 
        DeleteProductUseCase deleteProductUseCase,
        ImagesManagementServices imagesManagementServices)
    {
        this.notificationService = notificationService;
        this.productRepository = productRepository;
        this.deleteProductUseCase = deleteProductUseCase;
        this.imagesManagementServices = imagesManagementServices;
    }
    [Fact]
    public async Task ShouldSucess()
    {
        var Image = System.Text.Encoding.UTF8.GetBytes(MessageLogs.GabrielSymbol());
        var product = ProductBuilder.New().Build();
        productRepository.Add(product);
        var request = new DeleteProductRequest() { Product = product, Id = product.Id };
        request.Product.Image = imagesManagementServices.SaveImage(Image);
        await deleteProductUseCase.Execute(request);
        var imagePath = Path.Combine(Environment.GetEnvironmentVariable("ImagesPathByServiceInfra")!, request.Product.Image);
        File.Exists(imagePath).Should().BeFalse();
        notificationService.HasNotifications.Should().BeFalse();
        request.IsError.Should().BeFalse();
        request.output.Should().NotBeNull();
    }
}
