using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.UseCases.Products.CreateProduct;
using Telegram.BOT.Application.UseCases.Products.CreateProduct.Handlers;
using Telegram.BOT.Domain.Logs;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Application.Products.CreateProduct.Handlers;
[UseAutofacTestFramework]
public class SaveImageHandlerTest
{
    private readonly SaveImageHandler saveImageHandler;
    public SaveImageHandlerTest(SaveImageHandler saveImageHandler)
    {
        this.saveImageHandler = saveImageHandler;
    }
    [Fact]
    public async Task ShouldSucess()
    {
        var request = new CreateProductRequest() { Product = ProductBuilder.New().Build(), Image = System.Text.Encoding.UTF8.GetBytes(MessageLogs.GabrielSymbol())};
        await saveImageHandler.ProcessRequest(request);
        var imagePath = Path.Combine(Environment.GetEnvironmentVariable("ImagesPathByServiceInfra")!,request.Product.Image);
        File.Exists(imagePath).Should().BeTrue();
    }
}
