using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Marc.DeleteMarc;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Application.Marc.DeleteMarc;
[UseAutofacTestFramework]
public class DeleteMarcUseCaseTest
{
    private readonly DeleteMarcUseCase deleteMarcUseCase;
    private readonly IMarcRepository MarcRepository;
    public DeleteMarcUseCaseTest
        (DeleteMarcUseCase deleteMarcUseCase,
        IMarcRepository MarcRepository)
    {
        this.deleteMarcUseCase = deleteMarcUseCase;
        this.MarcRepository = MarcRepository;
    }
    [Fact]
    public async Task ShouldSucess()
    {
        var entity = MarcBuilder.New().Build();
        MarcRepository.Add(entity);
        var request = new DeleteMarcRequest() { Id = Guid.NewGuid() };
        await deleteMarcUseCase.Execute(request);
        request.output.Should().NotBeNull();
        request.IsError.Should().BeFalse();
    }
}
