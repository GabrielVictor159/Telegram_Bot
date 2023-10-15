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
    public class GetMarcHandlerTest
    {
        private readonly GetMarcHandler handler;
        private readonly IMarcRepository MarcRepository;
        private readonly NotificationService notificationService;

        public GetMarcHandlerTest(GetMarcHandler handler, IMarcRepository MarcRepository, NotificationService notificationService)
        {
            this.handler = handler;
            this.MarcRepository = MarcRepository;
            this.notificationService = notificationService;
        }

        [Fact]
        public async Task Should_Execute_Successfully()
        {
            var Marc = MarcBuilder.New().Build();
            MarcRepository.Add(Marc);

            var request = new UpdateMarcRequest
            {
                Marc = Marc
            };

            await handler.ProcessRequest(request);

            request.Logs.Should().NotBeEmpty();
            request.Marc.Should().BeEquivalentTo(Marc);
            notificationService.HasNotifications.Should().BeFalse();
        }

        [Fact]
        public async Task Should_Add_Notification_If_Marc_Is_Not_Found()
        {
            var request = new UpdateMarcRequest
            {
                Marc = new BOT.Domain.Products.Marc { Id = Guid.NewGuid(), Name ="Get Marc Not Found", CategoryId = Guid.NewGuid() }
            };

            await handler.ProcessRequest(request);

            notificationService.HasNotifications.Should().BeTrue();
            notificationService.Notifications.Should().Contain(notification => notification.Property == "Not Found");
        }
    }
}
