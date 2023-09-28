using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Application.UseCases.Marc.GetMarc;
using Telegram.BOT.Application.UseCases.Marc.GetMarc.Handlers;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Application.Marc.GetMarc.Handlers;
[UseAutofacTestFramework]
public class GetMarcHandlerTest
{
    private readonly GetMarcHandler getMarcHandler;
    private readonly INotificationService notificationService;
    private readonly IMarcRepository MarcRepository;
    public GetMarcHandlerTest
        (GetMarcHandler getMarcHandler,
        INotificationService notificationService,
        IMarcRepository MarcRepository)
    {
        this.getMarcHandler = getMarcHandler;
        this.notificationService = notificationService;
        this.MarcRepository = MarcRepository;
    }
    [Fact]
    public async Task ShouldSucess()
    {
        var entity = MarcBuilder.New().Build();
        MarcRepository.Add(entity);
        var request = new GetMarcRequest() { Name = entity.Name };
        await getMarcHandler.ProcessRequest(request);
        request.Logs.Should().NotBeEmpty();
        request.Marcs.Should().NotBeEmpty();
    }
}
