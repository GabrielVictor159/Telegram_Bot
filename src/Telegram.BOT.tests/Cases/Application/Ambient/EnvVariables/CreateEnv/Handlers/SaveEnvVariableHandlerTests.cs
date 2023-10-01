using ManagementServices.variables.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.CreateEnv.Handlers;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.CreateEnv;
using Xunit;
using Xunit.Frameworks.Autofac;
using ManagementServices.variables.Domain.Models;
using FluentAssertions;

namespace Telegram.BOT.tests.Cases.Application.Ambient.EnvVariables.CreateEnv.Handlers
{
    [UseAutofacTestFramework]
    public class SaveEnvVariableHandlerTests
    {
        private readonly IEnvVariableRepository envVariableRepository;
        private readonly SaveEnvVariableHandler saveEnvVariableHandler;
        public SaveEnvVariableHandlerTests
            (IEnvVariableRepository envVariableRepository,
            SaveEnvVariableHandler saveEnvVariableHandler)
        {
            this.envVariableRepository = envVariableRepository;
            this.saveEnvVariableHandler = saveEnvVariableHandler;
        }

        [Fact]
        public async Task Should_Save_EnvVariable_Successfully()
        {
            var request = new CreateEnvRequest
            {
                EnvVariable = new EnvVariable { Key = "SaveEnvVariableHandlerTests", Value = "VALUE" }
            };
            await saveEnvVariableHandler.ProcessRequest(request);
            var envVariable = envVariableRepository.Get(request.EnvVariable.Key);
            envVariable.Should().NotBeNull();
            envVariable!.Value.Should().Be("VALUE");
        }
    }
}
