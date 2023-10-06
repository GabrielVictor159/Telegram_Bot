using ManagementServices.variables.Infrastructure.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.GetEnv;
using Telegram.BOT.Infrastructure.Service;
using Xunit.Frameworks.Autofac;
using Xunit;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.GetEnv.Handlers;
using ManagementServices.variables.Application.Interfaces;
using ManagementServices.variables.Domain.Models;
using FluentAssertions;

namespace Telegram.BOT.tests.Cases.Application.Ambient.EnvVariables.GetEnv
{
    [UseAutofacTestFramework]
    public class GetEnvUseCaseTests
    {
        private readonly GetEnvUseCase useCase;
        private readonly NotificationService notificationService;
        private readonly IEnvVariableRepository envVariableRepository;

        public GetEnvUseCaseTests
            (GetEnvUseCase useCase, 
            NotificationService notificationService, 
            IEnvVariableRepository envVariableRepository)
        {
            this.useCase = useCase;
            this.notificationService = notificationService;
            this.envVariableRepository = envVariableRepository;
        }

        [Fact]
        public async Task Should_Execute_Successfully()
        {
            var envVariable = new EnvVariable
            {
                Key = "GetEnvUseCaseTestsSuccess",
                Value = "VALUE"
            };
            envVariableRepository.Add(envVariable);

            var request = new GetEnvRequest
            {
                Key = "GetEnvUseCaseTestsSuccess"
            };

            await useCase.Execute(request);

            request.Logs.Should().NotBeNullOrEmpty();
            request.output.Should().NotBeNull();
            request.output!.Key.Should().Be("GetEnvUseCaseTestsSuccess");
            request.output.Value.Should().Be("VALUE");
        }

        [Fact]
        public async Task Should_Add_Notification_If_Invalid_Key()
        {
            var request = new GetEnvRequest
            {
                Key = ""
            };

            await useCase.Execute(request);

            request.Logs.Should().NotBeNullOrEmpty();
            notificationService.HasNotifications.Should().BeTrue();
            notificationService.Notifications.Should().NotBeNullOrEmpty();
        }
    }
}
