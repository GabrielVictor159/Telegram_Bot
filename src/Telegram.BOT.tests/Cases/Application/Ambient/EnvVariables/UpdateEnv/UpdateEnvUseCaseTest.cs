using FluentAssertions;
using ManagementServices.variables.Application.Interfaces;
using ManagementServices.variables.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.RemoveEnv;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.UpdateEnv;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Application.Ambient.EnvVariables.UpdateEnv
{
    [UseAutofacTestFramework]
    public class UpdateEnvUseCaseTest
    {
        private readonly IEnvVariableRepository envVariableRepository;
        private readonly IUpdateEnvRequest updateEnvRequest;

        public UpdateEnvUseCaseTest(IEnvVariableRepository envVariableRepository, IUpdateEnvRequest updateEnvRequest)
        {
            this.envVariableRepository = envVariableRepository;
            this.updateEnvRequest = updateEnvRequest;
        }
        [Fact]
        public void ShouldSucess()
        {
            var entity = new EnvVariable() { Key = "UpdateEnvUseCaseTest", Value = "value Test" };
            envVariableRepository.Add(entity);
            var request = new UpdateEnvRequest() { variable = new EnvVariable() { Key = entity.Key, Value = "value test2" } };
            updateEnvRequest.Execute(request);
            request.IsError.Should().BeFalse();
            request.ErrorMessage.Should().BeEmpty();
            request.output.Should().NotBeNullOrEmpty();
        }
    }
}
