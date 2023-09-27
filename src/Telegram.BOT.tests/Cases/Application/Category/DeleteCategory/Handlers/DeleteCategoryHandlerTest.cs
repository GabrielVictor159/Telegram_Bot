using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Application.UseCases.Category.DeleteCategory;
using Telegram.BOT.Application.UseCases.Category.DeleteCategory.Handlers;
using Telegram.BOT.Domain;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Application.Category.DeleteCategory.Handlers;
[UseAutofacTestFramework]
public class DeleteCategoryHandlerTest
{
    private readonly DeleteCategoryHandler deleteCategoryHandler;
    private readonly ICategoryRepository categoryRepository;
    private readonly INotificationService notificationService;
    public DeleteCategoryHandlerTest
        (DeleteCategoryHandler deleteCategoryHandler,
        ICategoryRepository categoryRepository,
        INotificationService notificationService)
    {
        this.deleteCategoryHandler = deleteCategoryHandler;
        this.categoryRepository = categoryRepository;
        this.notificationService = notificationService;
    }
    [Fact]
    public async Task ShouldSucess()
    {
        var entity = CategoryBuilder.New().Build();
        categoryRepository.Add(entity);
        var request = new DeleteCategoryRequest() { Id = entity.Id};
        await deleteCategoryHandler.ProcessRequest(request);
        notificationService.HasNotifications.Should().BeFalse();
    }
    [Fact]
    public async Task ShouldFailureByNotFoundId()
    {
        var request = new DeleteCategoryRequest() { Id = Guid.NewGuid() };
        await deleteCategoryHandler.ProcessRequest(request);
        notificationService.HasNotifications.Should().BeTrue();
    }
}
