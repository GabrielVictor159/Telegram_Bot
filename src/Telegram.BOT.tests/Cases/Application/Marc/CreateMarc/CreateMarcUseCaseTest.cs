using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Application.UseCases.Marc.CreateMarc;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Application.Marc.CreateMarc;
[UseAutofacTestFramework]
public class CreateMarcUseCaseTest
{
    private readonly CreateMarcUseCase createMarcUseCase;
    private readonly IMarcRepository MarcRepository;
    private readonly INotificationService notificationService;
    public CreateMarcUseCaseTest
        (CreateMarcUseCase createMarcUseCase,
        IMarcRepository MarcRepository,
        INotificationService notificationService)
    {
        this.createMarcUseCase = createMarcUseCase;
        this.MarcRepository = MarcRepository;
        this.notificationService = notificationService;
    }
    [Fact]
    public async Task ShouldSucess()
    {
        var request = new CreateMarcRequest() { marc = MarcBuilder.New().Build() };
        await createMarcUseCase.Execute(request);
        notificationService.HasNotifications.Should().BeFalse();
        request.Logs.Should().NotBeEmpty();
        MarcRepository.GetByFilter(e => e.Id == request.marc.Id, 1, 10).FirstOrDefault().Should().NotBeNull();
    }
    [Fact]
    public async Task ShouldFailure()
    {
        var request = new CreateMarcRequest() { marc = MarcBuilder.New().WithName("").Build() };
        await createMarcUseCase.Execute(request);
        notificationService.HasNotifications.Should().BeTrue();
        request.Logs.Should().NotBeEmpty();
        MarcRepository.GetByFilter(e => e.Id == request.marc.Id, 1, 10).FirstOrDefault().Should().BeNull();
    }
}
