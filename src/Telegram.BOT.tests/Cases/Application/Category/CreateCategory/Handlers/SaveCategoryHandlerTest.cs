using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Category.CreateCategory;
using Telegram.BOT.Application.UseCases.Category.CreateCategory.Handlers;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Application.Category.CreateCategory.Handlers;
[UseAutofacTestFramework]
public class SaveCategoryHandlerTest
{
    private readonly SaveCategoryHandler saveCategoryHandler;
    private readonly ICategoryRepository categoryRepository;
    public SaveCategoryHandlerTest(SaveCategoryHandler saveCategoryHandler, ICategoryRepository categoryRepository)
    {
        this.saveCategoryHandler = saveCategoryHandler;
        this.categoryRepository = categoryRepository;
    }
    [Fact]
    public async Task ShouldSucess()
    {
        var request = new CreateCategoryRequest() { category = CategoryBuilder.New().Build() };
        await saveCategoryHandler.ProcessRequest(request);
        request.Logs.Should().NotBeEmpty();
        categoryRepository.GetByFilter(e=>e.Id==request.category.Id,1,10).FirstOrDefault().Should().NotBeNull();

    }
}
