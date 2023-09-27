using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Marc.CreateMarc;
using Telegram.BOT.Application.UseCases.Marc.CreateMarc.Handlers;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Application.Marc.CreateMarc.Handlers;
[UseAutofacTestFramework]
public class SaveMarcHandlerTest
{
    private readonly SaveMarcHandler saveMarcHandler;
    private readonly IMarcRepository MarcRepository;
    public SaveMarcHandlerTest
        (SaveMarcHandler saveMarcHandler, 
        IMarcRepository MarcRepository)
    {
        this.saveMarcHandler = saveMarcHandler;
        this.MarcRepository = MarcRepository;
    }
    [Fact]
    public async Task ShouldSucess()
    {
        var request = new CreateMarcRequest() { marc = MarcBuilder.New().Build() };
        await saveMarcHandler.ProcessRequest(request);
        request.Logs.Should().NotBeEmpty();
        MarcRepository.GetByFilter(e => e.Id == request.marc.Id, 1, 10).FirstOrDefault().Should().NotBeNull();

    }
}
