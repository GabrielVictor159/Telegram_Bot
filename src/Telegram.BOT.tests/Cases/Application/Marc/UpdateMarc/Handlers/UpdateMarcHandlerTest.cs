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
    public class UpdateMarcHandlerTest
    {
        private readonly UpdateMarcHandler handler;
        private readonly IMarcRepository MarcRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly NotificationService notificationService;

        public UpdateMarcHandlerTest
            (UpdateMarcHandler handler, 
            IMarcRepository MarcRepository, 
            NotificationService notificationService,
            ICategoryRepository categoryRepository)
        {
            this.handler = handler;
            this.MarcRepository = MarcRepository;
            this.notificationService = notificationService;
            this.categoryRepository = categoryRepository;
        }

        [Fact]
        public async Task Should_Execute_Successfully()
        {
            var Marc = MarcBuilder.New().Build();
            var newCategory = CategoryBuilder.New().Build();
            MarcRepository.Add(Marc);
            categoryRepository.Add(newCategory);
            var request = new UpdateMarcRequest
            {
                Marc = new BOT.Domain.Products.Marc { Id = Marc.Id, Name = "Updated Marc", CategoryId = newCategory.Id }
            };

            await handler.ProcessRequest(request);

            request.Logs.Should().NotBeEmpty();
            request.IsError.Should().BeFalse();
            request.ErrorMessage.Should().Be("");
        }


    }
}
