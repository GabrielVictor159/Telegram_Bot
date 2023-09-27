using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Application.UseCases.Category.GetCategory;
using Telegram.BOT.Application.UseCases.Category.GetCategory.Handlers;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Application.Category.GetCategory.Handlers;

[UseAutofacTestFramework]
public class GetCategoryHandlerTest
{
    private readonly GetCategoryHandler getCategoryHandler;
    private readonly INotificationService notificationService;
    private readonly ICategoryRepository categoryRepository;
    public GetCategoryHandlerTest
        (GetCategoryHandler getCategoryHandler,
        INotificationService notificationService,
        ICategoryRepository categoryRepository)
    {
        this.getCategoryHandler = getCategoryHandler;
        this.notificationService = notificationService;
        this.categoryRepository = categoryRepository;
    }
    [Fact]
    public async Task ShouldSucess()
    {
        var entity = CategoryBuilder.New().Build();
        categoryRepository.Add(entity);
        var request = new GetCategoryRequest() {Name = entity.Name };
        await getCategoryHandler.ProcessRequest(request);
        request.Logs.Should().NotBeEmpty();
        request.Categories.Should().NotBeEmpty();
    }
}
