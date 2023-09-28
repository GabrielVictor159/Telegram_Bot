using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Marc.GetMarc;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Application.Marc.GetMarc;
[UseAutofacTestFramework]
public class GetMarcUseCaseTest
{
    private readonly GetMarcUseCase getMarcUseCase;
    private readonly IMarcRepository MarcRepository;
    public GetMarcUseCaseTest
        (GetMarcUseCase getMarcUseCase,
        IMarcRepository MarcRepository)
    {
        this.MarcRepository = MarcRepository;
        this.getMarcUseCase = getMarcUseCase;
    }
    [Fact]
    public async Task ShouldSucess()
    {
        var entity = MarcBuilder.New().Build();
        MarcRepository.Add(entity);
        var request = new GetMarcRequest() { Name = entity.Name };
        await getMarcUseCase.Execute(request);
        request.IsError.Should().BeFalse();
        request.output.Should().NotBeNull();
    }
}
