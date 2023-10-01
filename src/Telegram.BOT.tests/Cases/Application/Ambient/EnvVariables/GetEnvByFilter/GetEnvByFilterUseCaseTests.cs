using ManagementServices.variables.Infrastructure.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Boundaries.Ambient.EnvVariables;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.GetEnvByFilter.Handlers;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.GetEnvByFilter;
using Xunit.Frameworks.Autofac;
using Xunit;
using ManagementServices.variables.Domain.Models;
using ManagementServices.variables.Application.Interfaces;
using FluentAssertions;

namespace Telegram.BOT.tests.Cases.Application.Ambient.EnvVariables.GetEnvByFilter
{
    [UseAutofacTestFramework]
    public class GetEnvByFilterUseCaseTests
    {
        private readonly GetEnvByFilterUseCase useCase;
        private readonly IEnvVariableRepository envVariableRepository;

        public GetEnvByFilterUseCaseTests
            (GetEnvByFilterUseCase useCase, 
            IEnvVariableRepository envVariableRepository)
        {
            this.useCase = useCase;
            this.envVariableRepository = envVariableRepository;
        }

        [Fact]
        public async Task Should_Execute_Successfully()
        {
            var envVariable1 = new EnvVariable
            {
                Key = "GetEnvByFilterUseCaseTestsSuccess1",
                Value = "VALUE1"
            };
            envVariableRepository.Add(envVariable1);

            var envVariable2 = new EnvVariable
            {
                Key = "GetEnvByFilterUseCaseTestsSuccess2",
                Value = "VALUE2"
            };
            envVariableRepository.Add(envVariable2);

            var request = new GetEnvByFilterRequest
            {
                Key = "GetEnvByFilterUseCaseTestsSuccess",
                Value = "VALUE"
            };

            await useCase.Execute(request);

            request.output.Should().NotBeNullOrEmpty();
        }
    }
}
