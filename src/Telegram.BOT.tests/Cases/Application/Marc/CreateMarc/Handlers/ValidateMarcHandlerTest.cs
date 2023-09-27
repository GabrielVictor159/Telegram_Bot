using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Application.UseCases.Marc.CreateMarc;
using Telegram.BOT.Application.UseCases.Marc.CreateMarc.Handlers;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Application.Marc.CreateMarc.Handlers;
[UseAutofacTestFramework]
public class ValidateMarcHandlerTest
{
    private readonly ValidateMarcHandler validateMarcHandler;
    private readonly INotificationService notificationService;
    public ValidateMarcHandlerTest(ValidateMarcHandler validateMarcHandler, INotificationService notificationService)
    {
        this.validateMarcHandler = validateMarcHandler;
        this.notificationService = notificationService;
    }
    [Fact]
    public async Task ShouldSucessValidDomain()
    {
        await validateMarcHandler.ProcessRequest(new CreateMarcRequest() { marc = MarcBuilder.New().Build() });
        notificationService.HasNotifications.Should().BeFalse();
    }
    [Fact]
    public async Task ShouldFailedInValidDomain()
    {
        await validateMarcHandler.ProcessRequest(new CreateMarcRequest() { marc = MarcBuilder.New().WithName("").Build() });
        notificationService.HasNotifications.Should().BeTrue();
    }
}
