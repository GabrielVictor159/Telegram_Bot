using FluentAssertions;
using Microsoft.AspNetCore.Server.HttpSys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Category.DeleteCategory;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Application.Category.DeleteCategory;

[UseAutofacTestFramework]
public class DeleteCategoryUseCaseTest
{
    private readonly DeleteCategoryUseCase deleteCategoryUseCase;
    private readonly ICategoryRepository categoryRepository;
    public DeleteCategoryUseCaseTest
        (DeleteCategoryUseCase deleteCategoryUseCase,
        ICategoryRepository categoryRepository)
    {
        this.deleteCategoryUseCase = deleteCategoryUseCase;
        this.categoryRepository = categoryRepository;
    }
    [Fact]
    public async Task ShouldSucess()
    {
        var entity = CategoryBuilder.New().Build(); 
        categoryRepository.Add(entity);
        var request = new DeleteCategoryRequest() { Id = Guid.NewGuid() };
        await deleteCategoryUseCase.Execute(request);
        request.output.Should().NotBeNull();
        request.IsError.Should().BeFalse();  
    }
}
