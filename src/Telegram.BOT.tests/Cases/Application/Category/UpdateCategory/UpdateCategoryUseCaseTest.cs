using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Boundaries.Category;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Category.UpdateCategory.Handlers;
using Telegram.BOT.Domain.Enums;
using Telegram.BOT.Application.UseCases.Category.UpdateCategory;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;
using Telegram.BOT.Application.Interfaces.Services;

namespace Telegram.BOT.tests.Cases.Application.Category.UpdateCategory
{
    [UseAutofacTestFramework]
    public class UpdateCategoryUseCaseTest
    {
        private readonly UpdateCategoryUseCase updateCategoryUseCase;
        private readonly ICategoryRepository categoryRepository;
        private readonly INotificationService notificationService;

        public UpdateCategoryUseCaseTest
            (UpdateCategoryUseCase updateCategoryUseCase, 
            ICategoryRepository categoryRepository, 
            INotificationService notificationService)
        {
            this.updateCategoryUseCase = updateCategoryUseCase;
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

            await updateCategoryUseCase.Execute(request);

            request.Logs.Should().NotBeEmpty();

            request.output!.Category.Should().BeEquivalentTo(request.Category);

        }

        [Fact]
        public async Task Should_Add_Notification_If_Category_Is_Not_Found()
        {
            var request = new UpdateCategoryRequest
            {
                Category = new BOT.Domain.Products.Category { Id = Guid.NewGuid(), Name="Update Category not found" }
            };

            await updateCategoryUseCase.Execute(request);

            request.Logs.Should().NotBeEmpty();

            notificationService.HasNotifications.Should().BeTrue();
            notificationService.Notifications.Should().Contain(e=>e.Property=="Not Found");
        }
    }
}
