using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Domain.Enums;
using Telegram.BOT.Application.UseCases.Category.UpdateCategory.Handlers;
using Telegram.BOT.tests.Builder.Domain.Product;
using Telegram.BOT.Application.UseCases.Category.UpdateCategory;
using Telegram.BOT.Infrastructure.Service;
using Xunit.Frameworks.Autofac;
using Xunit;

namespace Telegram.BOT.tests.Cases.Application.Category.UpdateCategory.Handlers
{
    [UseAutofacTestFramework]
    public class UpdateCategoryHandlerTest
    {
        private readonly UpdateCategoryHandler handler;
        private readonly ICategoryRepository categoryRepository;
        private readonly NotificationService notificationService;

        public UpdateCategoryHandlerTest(UpdateCategoryHandler handler, ICategoryRepository categoryRepository, NotificationService notificationService)
        {
            this.handler = handler;
            this.categoryRepository = categoryRepository;
            this.notificationService = notificationService;
        }

        [Fact]
        public async Task Should_Execute_Successfully()
        {
            var category = CategoryBuilder.New().Build();
            categoryRepository.Add(category);

            var request = new UpdateCategoryRequest
            {
                Category = new BOT.Domain.Products.Category { Id = category.Id, Name = "Updated Category" }
            };

            await handler.ProcessRequest(request);

            request.Logs.Should().NotBeEmpty();
            request.IsError.Should().BeFalse();
            request.ErrorMessage.Should().Be("");
        }


    }
}
