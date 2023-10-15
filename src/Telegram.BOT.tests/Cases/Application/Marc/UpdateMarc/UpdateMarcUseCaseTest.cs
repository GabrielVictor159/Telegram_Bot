using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Boundaries.Marc;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Marc.UpdateMarc.Handlers;
using Telegram.BOT.Domain.Enums;
using Telegram.BOT.Application.UseCases.Marc.UpdateMarc;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;
using Telegram.BOT.Application.Interfaces.Services;

namespace Telegram.BOT.tests.Cases.Application.Marc.UpdateMarc
{
    [UseAutofacTestFramework]
    public class UpdateMarcUseCaseTest
    {
        private readonly UpdateMarcUseCase updateMarcUseCase;
        private readonly IMarcRepository MarcRepository;
        private readonly INotificationService notificationService;

        public UpdateMarcUseCaseTest
            (UpdateMarcUseCase updateMarcUseCase, 
            IMarcRepository MarcRepository, 
            INotificationService notificationService)
        {
            this.updateMarcUseCase = updateMarcUseCase;
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
                Marc = new BOT.Domain.Products.Marc { Id = Marc.Id, Name = "Updated Marc", CategoryId = Marc.CategoryId }
            };

            await updateMarcUseCase.Execute(request);

            request.Logs.Should().NotBeEmpty();

            request.output!.Marc.Should().BeEquivalentTo(request.Marc);

        }

        [Fact]
        public async Task Should_Add_Notification_If_Marc_Is_Not_Found()
        {
            var request = new UpdateMarcRequest
            {
                Marc = new BOT.Domain.Products.Marc { Id = Guid.NewGuid(), Name="Update Marc not found", CategoryId = Guid.NewGuid() }
            };

            await updateMarcUseCase.Execute(request);

            request.Logs.Should().NotBeEmpty();

            notificationService.HasNotifications.Should().BeTrue();
            notificationService.Notifications.Should().Contain(e=>e.Property=="Not Found");
        }
    }
}
