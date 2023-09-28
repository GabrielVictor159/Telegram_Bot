using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Application.UseCases.Products.CreateProduct;
using Telegram.BOT.Application.UseCases.Products.CreateProduct.Handlers;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Application.Products.CreateProduct.Handlers;
[UseAutofacTestFramework]
public class ValidateProductHandlerTest
{
    private readonly INotificationService notificationService;
    private readonly ValidateProductHandler validateProductHandler;
    public ValidateProductHandlerTest
        (INotificationService notificationService,
        ValidateProductHandler validateProductHandler)
    {
        this.validateProductHandler = validateProductHandler;
        this.notificationService = notificationService;
    }
    [Fact]
    public async Task ShouldInvalidDomain()
    {
        var request = new CreateProductRequest() { Product = ProductBuilder.New().WithName("").Build(), Image = new byte[10] };
        await validateProductHandler.ProcessRequest(request);
        notificationService.HasNotifications.Should().BeTrue();
    }
}
