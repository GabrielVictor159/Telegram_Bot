using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Domain.Enums;
using Telegram.BOT.Application.UseCases.Marc.UpdateMarc.Handlers;
using Telegram.BOT.tests.Builder.Domain.Product;
using Telegram.BOT.Application.UseCases.Marc.UpdateMarc;
using Telegram.BOT.Infrastructure.Service;
using Xunit.Frameworks.Autofac;
using Xunit;

namespace Telegram.BOT.tests.Cases.Application.Marc.UpdateMarc.Handlers
{
    [UseAutofacTestFramework]
    public class VerifyNameDisponibilityHandlerTest
    {
        private readonly VerifyNameDisponibilityHandler handler;
        private readonly IMarcRepository MarcRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly NotificationService notificationService;

        public VerifyNameDisponibilityHandlerTest
            (VerifyNameDisponibilityHandler handler, 
            IMarcRepository MarcRepository, 
            ICategoryRepository categoryRepository, 
            NotificationService notificationService)
        {
            this.handler = handler;
            this.MarcRepository = MarcRepository;
            this.categoryRepository = categoryRepository;
            this.notificationService = notificationService;
        }

        [Fact]
        public async Task Should_Execute_Successfully_When_Name_Is_Available()
        {
            var Marc = MarcBuilder.New().Build();
            MarcRepository.Add(Marc);

            var request = new UpdateMarcRequest
            {
                Marc = new BOT.Domain.Products.Marc { Id = Guid.NewGuid(), Name = "New Marc", CategoryId = Guid.NewGuid() }
            };

            await handler.ProcessRequest(request);

            request.Logs.Should().NotBeEmpty();
            notificationService.HasNotifications.Should().BeFalse();
        }

        [Fact]
        public async Task Should_Add_Notification_If_Name_Is_Already_Used_By_A_Category()
        {
            var Marc = MarcBuilder.New().Build();
            var category = CategoryBuilder.New().WithName(Marc.Name).Build();   
            MarcRepository.Add(Marc);
            categoryRepository.Add(category);

            var request = new UpdateMarcRequest
            {
                Marc = new BOT.Domain.Products.Marc { Id = Guid.NewGuid(), Name = category.Name , CategoryId = category.Id}
            };

            await handler.ProcessRequest(request);

            notificationService.HasNotifications.Should().BeTrue();
            notificationService.Notifications.Should().Contain(notification => notification.Property == "Name Using" );
        }

        [Fact]
        public async Task Should_Add_Notification_If_Name_Is_Already_Used_By_Another_Marc()
        {
            var Marc = MarcBuilder.New().Build();
            var category = CategoryBuilder.New().WithName(Marc.Name).Build();
            MarcRepository.Add(Marc);
            categoryRepository.Add(category);

            var request = new UpdateMarcRequest
            {
                Marc = new BOT.Domain.Products.Marc { Id = Guid.NewGuid(), Name = Marc.Name, CategoryId = category.Id }
            };

            await handler.ProcessRequest(request);

            notificationService.HasNotifications.Should().BeTrue();
            notificationService.Notifications.Should().Contain(notification => notification.Property == "Name Using" );
        }
    }
}
