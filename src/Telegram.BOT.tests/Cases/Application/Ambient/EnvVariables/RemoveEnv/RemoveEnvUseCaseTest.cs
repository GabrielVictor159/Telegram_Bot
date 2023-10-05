using FluentAssertions;
using ManagementServices.variables.Application.Interfaces;
using ManagementServices.variables.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.RemoveEnv;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Application.Ambient.EnvVariables.RemoveEnv
{
    [UseAutofacTestFramework]
    public class RemoveEnvUseCaseTest
    {
        private readonly IEnvVariableRepository envVariableRepository;
        private readonly IRemoveEnvRequest removeEnvRequest;

        public RemoveEnvUseCaseTest(IEnvVariableRepository envVariableRepository, IRemoveEnvRequest removeEnvRequest)
        {
            this.envVariableRepository = envVariableRepository;
            this.removeEnvRequest = removeEnvRequest;
        }
        [Fact]
        public void ShouldSucess()
        {
            var entity = new EnvVariable() { Key = "variableTestRemoveEnvUseCase", Value = "value Test" };
            envVariableRepository.Add(entity);  
            var request = new RemoveEnvRequest() { Key = entity.Key};
            removeEnvRequest.Execute(request);
            request.IsError.Should().BeFalse();
            request.ErrorMessage.Should().BeEmpty();
            request.output.Should().NotBeNullOrEmpty();
        }
    }
}
