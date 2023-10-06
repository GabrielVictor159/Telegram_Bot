using ManagementServices.variables.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.GetEnvByFilter.Handlers;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.GetEnvByFilter;
using Xunit.Frameworks.Autofac;
using Xunit;
using ManagementServices.variables.Domain.Models;
using FluentAssertions;

namespace Telegram.BOT.tests.Cases.Application.Ambient.EnvVariables.GetEnvByFilter.Handlers
{
    [UseAutofacTestFramework]
    public class GetEnvVariablesHandlerTests
    {
        private readonly IEnvVariableRepository envVariableRepository;
        private readonly GetEnvVariablesHandler handler;

        public GetEnvVariablesHandlerTests(IEnvVariableRepository envVariableRepository, GetEnvVariablesHandler handler)
        {
            this.envVariableRepository = envVariableRepository;
            this.handler = handler;
        }

        [Fact]
        public async Task Should_Execute_Successfully()
        {
            var envVariable1 = new EnvVariable
            {
                Key = "GetEnvVariablesHandlerTestsSuccess1",
                Value = "VALUE1"
            };
            envVariableRepository.Add(envVariable1);

            var envVariable2 = new EnvVariable
            {
                Key = "GetEnvVariablesHandlerTestsSuccess2",
                Value = "VALUE2"
            };
            envVariableRepository.Add(envVariable2);

            var request = new GetEnvByFilterRequest
            {
                Key = "GetEnvVariablesHandlerTestsSuccess",
                Value = "VALUE"
            };

            await handler.ProcessRequest(request);
            request.variablesFound.Should().NotBeNullOrEmpty();
        }
    }
}
