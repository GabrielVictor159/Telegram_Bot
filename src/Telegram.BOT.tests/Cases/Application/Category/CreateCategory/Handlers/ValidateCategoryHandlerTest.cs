using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Application.UseCases.Category.CreateCategory;
using Telegram.BOT.Application.UseCases.Category.CreateCategory.Handlers;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Application.Category.CreateCategory.Handlers;
[UseAutofacTestFramework]
public class ValidateCategoryHandlerTest
{
    private readonly ValidateCategoryHandler validateCategoryHandler;
    private readonly INotificationService notificationService;
    public ValidateCategoryHandlerTest(ValidateCategoryHandler validateCategoryHandler, INotificationService notificationService)
    {
        this.validateCategoryHandler = validateCategoryHandler;
        this.notificationService = notificationService;
    }
    [Fact]
    public async Task ShouldSucessValidDomain()
    {
        await validateCategoryHandler.ProcessRequest(new CreateCategoryRequest() { category = CategoryBuilder.New().Build()});
        notificationService.HasNotifications.Should().BeFalse();
    }
    [Fact]
    public async Task ShouldFailedInValidDomain()
    {
        await validateCategoryHandler.ProcessRequest(new CreateCategoryRequest() { category = CategoryBuilder.New().WithName("").Build() });
        notificationService.HasNotifications.Should().BeTrue();
    }
}
