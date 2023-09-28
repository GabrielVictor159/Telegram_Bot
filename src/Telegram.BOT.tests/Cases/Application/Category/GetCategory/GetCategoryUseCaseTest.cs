using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Category.GetCategory;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Application.Category.GetCategory;

[UseAutofacTestFramework]
public class GetCategoryUseCaseTest
{
    private readonly GetCategoryUseCase getCategoryUseCase;
    private readonly ICategoryRepository categoryRepository;
    public GetCategoryUseCaseTest
        (GetCategoryUseCase getCategoryUseCase,
        ICategoryRepository categoryRepository)
    {
        this.categoryRepository = categoryRepository;
        this.getCategoryUseCase = getCategoryUseCase;
    }
    [Fact]
    public async Task ShouldSucess()
    {
        var entity = CategoryBuilder.New().Build();
        categoryRepository.Add(entity);
        var request = new GetCategoryRequest() {Name = entity.Name };
        await getCategoryUseCase.Execute(request);
        request.IsError.Should().BeFalse();
        request.output.Should().NotBeNull();
    }
}
