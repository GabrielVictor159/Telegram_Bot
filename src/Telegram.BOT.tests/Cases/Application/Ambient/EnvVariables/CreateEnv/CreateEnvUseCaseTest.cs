using ManagementServices.variables.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.CreateEnv.Handlers;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.CreateEnv;
using Telegram.BOT.Domain.Enums;
using Xunit;
using Xunit.Frameworks.Autofac;
using Telegram.BOT.Infrastructure.Service;
using ManagementServices.variables.Domain.Models;
using FluentAssertions;
using Telegram.BOT.Application.Interfaces.Services;

namespace Telegram.BOT.tests.Cases.Application.Ambient.EnvVariables.CreateEnv
{
    [UseAutofacTestFramework]
    public class CreateEnvUseCaseTest
    {
        private readonly IEnvVariableRepository envVariableRepository;
        private readonly CreateEnvUseCase useCase;
        private readonly NotificationService notificationService;

        public CreateEnvUseCaseTest(IEnvVariableRepository envVariableRepository, CreateEnvUseCase useCase, NotificationService notificationService)
        {
            this.envVariableRepository = envVariableRepository;
            this.useCase = useCase;
            this.notificationService = notificationService;
        }

        [Fact]
        public async Task Should_Execute_Successfully()
        {
            var request = new CreateEnvRequest
            {
                EnvVariable = new EnvVariable { Key = "CreateEnvUseCaseTestsSucess", Value = "VALUE" }
            };
            await useCase.Execute(request);
            envVariableRepository.Get(request.EnvVariable.Key).Should().NotBeNull();
            request.Logs.Should().NotBeNullOrEmpty();
            request.output.Should().Be("Sucess Register Enviorment");
        }

        [Fact]
        public async Task Should_Add_Notification_If_Error_Occurs_And_EnvVariable_Already_Exists()
        {
            var envVariable = new EnvVariable { Key = "CreateEnvUseCaseTests", Value = "VALUE" };
            envVariableRepository.Add(envVariable);

            var request = new CreateEnvRequest
            {
                EnvVariable = new EnvVariable { Key = "CreateEnvUseCaseTests", Value = "VALUE" }
            };

            await useCase.Execute(request);
            request.Logs.Should().NotBeNullOrEmpty();
            notificationService.HasNotifications.Should().BeTrue(); 
        }

        [Fact]
        public async Task Should_Add_Notification_If_EnvVariable_IsInvalid()
        {
            var request = new CreateEnvRequest
            {
                EnvVariable = new EnvVariable { Key = "", Value = "VALUE" }
            };

            await useCase.Execute(request);
            request.Logs.Should().NotBeNullOrEmpty();
            notificationService.HasNotifications.Should().BeTrue();
        }
    }
}
