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
    public class VerifyNameDisponibilityHandlerTest
    {
        private readonly VerifyNameDisponibilityHandler handler;
        private readonly ICategoryRepository categoryRepository;
        private readonly IMarcRepository marcRepository;
        private readonly NotificationService notificationService;

        public VerifyNameDisponibilityHandlerTest(VerifyNameDisponibilityHandler handler, ICategoryRepository categoryRepository, IMarcRepository marcRepository, NotificationService notificationService)
        {
            this.handler = handler;
            this.categoryRepository = categoryRepository;
            this.marcRepository = marcRepository;
            this.notificationService = notificationService;
        }

        [Fact]
        public async Task Should_Execute_Successfully_When_Name_Is_Available()
        {
            var category = CategoryBuilder.New().Build();
            categoryRepository.Add(category);

            var request = new UpdateCategoryRequest
            {
                Category = new BOT.Domain.Products.Category { Id = Guid.NewGuid(), Name = "New Category" }
            };

            await handler.ProcessRequest(request);

            request.Logs.Should().NotBeEmpty();
            notificationService.HasNotifications.Should().BeFalse();
        }

        [Fact]
        public async Task Should_Add_Notification_If_Name_Is_Already_Used_By_A_Brand()
        {
            var category = CategoryBuilder.New().Build();
            var marc = MarcBuilder.New().WithName(category.Name).Build();   
            categoryRepository.Add(category);
            marcRepository.Add(marc);

            var request = new UpdateCategoryRequest
            {
                Category = new BOT.Domain.Products.Category { Id = Guid.NewGuid(), Name = category.Name }
            };

            await handler.ProcessRequest(request);

            notificationService.HasNotifications.Should().BeTrue();
            notificationService.Notifications.Should().Contain(notification => notification.Property == "Name Using" && notification.Message == "There is already a brand with the same name");
        }

        [Fact]
        public async Task Should_Add_Notification_If_Name_Is_Already_Used_By_Another_Category()
        {
            var category = CategoryBuilder.New().Build();
            categoryRepository.Add(category);

            var request = new UpdateCategoryRequest
            {
                Category = new BOT.Domain.Products.Category { Id = Guid.NewGuid(), Name = category.Name }
            };

            await handler.ProcessRequest(request);

            notificationService.HasNotifications.Should().BeTrue();
            notificationService.Notifications.Should().Contain(notification => notification.Property == "Name Using" && notification.Message == "A category with the same name already exists");
        }
    }
}
