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
    public class GetCategoryHandlerTest
    {
        private readonly GetCategoryHandler handler;
        private readonly ICategoryRepository categoryRepository;
        private readonly NotificationService notificationService;

        public GetCategoryHandlerTest(GetCategoryHandler handler, ICategoryRepository categoryRepository, NotificationService notificationService)
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
                Category = category
            };

            await handler.ProcessRequest(request);

            request.Logs.Should().NotBeEmpty();
            request.Category.Should().BeEquivalentTo(category);
            notificationService.HasNotifications.Should().BeFalse();
        }

        [Fact]
        public async Task Should_Add_Notification_If_Category_Is_Not_Found()
        {
            var request = new UpdateCategoryRequest
            {
                Category = new BOT.Domain.Products.Category { Id = Guid.NewGuid(), Name ="Get Category Not Found" }
            };

            await handler.ProcessRequest(request);

            notificationService.HasNotifications.Should().BeTrue();
            notificationService.Notifications.Should().Contain(notification => notification.Property == "Not Found");
        }
    }
}
