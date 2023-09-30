using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Application.UseCases.Products.CreateProduct;
using Telegram.BOT.Application.UseCases.Products.DeleteProduct;
using Telegram.BOT.Application.UseCases.Products.DeleteProduct.Handlers;
using Telegram.BOT.Domain.Logs;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Application.Products.DeleteProduct.Handlers;
[UseAutofacTestFramework]
public class DeleteImageProductHandlerTest
{
    private readonly IImagesManagementServices imagesManagementServices;
    private readonly DeleteImageProductHandler deleteImageProductHandler;

    public DeleteImageProductHandlerTest(IImagesManagementServices imagesManagementServices, DeleteImageProductHandler deleteImageProductHandler)
    {
        this.imagesManagementServices = imagesManagementServices;
        this.deleteImageProductHandler = deleteImageProductHandler;
    }

    [Fact]
    public async Task ShouldBeSucess()
    {
        var Image = System.Text.Encoding.UTF8.GetBytes(MessageLogs.GabrielSymbol());
        var product = ProductBuilder.New().Build();
        var request = new DeleteProductRequest() { Product = product, Id = product.Id };
        request.Product.Image = imagesManagementServices.SaveImage(Image);
        await deleteImageProductHandler.ProcessRequest(request);
        var imagePath = Path.Combine(Environment.GetEnvironmentVariable("ImagesPathByServiceInfra")!, request.Product.Image);
        File.Exists(imagePath).Should().BeFalse();
    }
}
