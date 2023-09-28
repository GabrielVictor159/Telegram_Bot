using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Application.UseCases.Products.CreateProduct;
using Telegram.BOT.Domain.Logs;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Application.Products.CreateProduct;
[UseAutofacTestFramework]
public class CreateProductUseCaseTest
{
    private readonly INotificationService notificationService;
    private readonly CreateProductUseCase createProductUseCase;
    public CreateProductUseCaseTest(INotificationService notificationService, CreateProductUseCase createProductUseCase)
    {
        this.notificationService = notificationService;
        this.createProductUseCase = createProductUseCase;
    }
    [Fact]
    public async Task ShouldExecuteSucess()
    {
        var request = new CreateProductRequest() { Product = ProductBuilder.New().Build(), Image = System.Text.Encoding.UTF8.GetBytes(MessageLogs.GabrielSymbol()) };
        await createProductUseCase.Execute(request);
        notificationService.HasNotifications.Should().BeFalse();
        request.IsError.Should().BeFalse();
        request.output.Should().NotBeNull();
    }
}
