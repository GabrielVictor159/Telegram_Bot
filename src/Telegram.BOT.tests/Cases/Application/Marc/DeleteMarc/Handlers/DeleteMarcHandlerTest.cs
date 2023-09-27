using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Application.UseCases.Marc.DeleteMarc.Handlers;
using Telegram.BOT.Application.UseCases.Marc.DeleteMarc;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;
using FluentAssertions;

namespace Telegram.BOT.tests.Cases.Application.Marc.DeleteMarc.Handlers;
[UseAutofacTestFramework]
public class DeleteMarcHandlerTest
{
    private readonly DeleteMarcHandler deleteMarcHandler;
    private readonly IMarcRepository MarcRepository;
    private readonly INotificationService notificationService;
    public DeleteMarcHandlerTest
        (DeleteMarcHandler deleteMarcHandler,
        IMarcRepository MarcRepository,
        INotificationService notificationService)
    {
        this.deleteMarcHandler = deleteMarcHandler;
        this.MarcRepository = MarcRepository;
        this.notificationService = notificationService;
    }
    [Fact]
    public async Task ShouldSucess()
    {
        var entity = MarcBuilder.New().Build();
        MarcRepository.Add(entity);
        var request = new DeleteMarcRequest() { Id = entity.Id };
        await deleteMarcHandler.ProcessRequest(request);
        notificationService.HasNotifications.Should().BeFalse();
    }
    [Fact]
    public async Task ShouldFailureByNotFoundId()
    {
        var request = new DeleteMarcRequest() { Id = Guid.NewGuid() };
        await deleteMarcHandler.ProcessRequest(request);
        notificationService.HasNotifications.Should().BeTrue();
    }
}
