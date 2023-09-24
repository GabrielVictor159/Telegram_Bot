using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Application.UseCases.Category.CreateCategory;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Application.Category.CreateCategory;
[UseAutofacTestFramework]
public class CreateCategoryUseCaseTest
{
    private readonly CreateCategoryUseCase createCategoryUseCase;
    private readonly ICategoryRepository categoryRepository;
    private readonly INotificationService notificationService;
    public CreateCategoryUseCaseTest
        (CreateCategoryUseCase createCategoryUseCase, 
        ICategoryRepository categoryRepository, 
        INotificationService notificationService)
    {
        this.createCategoryUseCase = createCategoryUseCase;
        this.categoryRepository = categoryRepository;
        this.notificationService = notificationService;
    }
    [Fact]
    public async Task ShouldSucess()
    {
        var request = new CreateCategoryRequest() { category = CategoryBuilder.New().Build() };
        await createCategoryUseCase.Execute(request);
        notificationService.HasNotifications.Should().BeFalse();
        request.Logs.Should().NotBeEmpty();
        categoryRepository.GetByFilter(e => e.Id == request.category.Id, 1, 10).FirstOrDefault().Should().NotBeNull();
    }
    [Fact]
    public async Task ShouldFailure()
    {
        var request = new CreateCategoryRequest() { category = CategoryBuilder.New().WithName("").Build() };
        await createCategoryUseCase.Execute(request);
        notificationService.HasNotifications.Should().BeTrue();
        request.Logs.Should().NotBeEmpty();
        categoryRepository.GetByFilter(e => e.Id == request.category.Id, 1, 10).FirstOrDefault().Should().BeNull();
    }
}
