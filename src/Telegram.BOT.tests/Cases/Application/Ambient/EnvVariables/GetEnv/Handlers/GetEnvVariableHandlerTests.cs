using ManagementServices.variables.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.GetEnv;
using Telegram.BOT.Infrastructure.Service;
using Xunit.Frameworks.Autofac;
using Xunit;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.GetEnv.Handlers;
using ManagementServices.variables.Domain.Models;
using FluentAssertions;

namespace Telegram.BOT.tests.Cases.Application.Ambient.EnvVariables.GetEnv.Handlers
{
    [UseAutofacTestFramework]
    public class GetEnvVariableHandlerTests
    {
        private readonly IEnvVariableRepository envVariableRepository;
        private readonly GetEnvVariableHandler handler;
        private readonly NotificationService notificationService;

        public GetEnvVariableHandlerTests(IEnvVariableRepository envVariableRepository, GetEnvVariableHandler handler, NotificationService notificationService)
        {
            this.envVariableRepository = envVariableRepository;
            this.handler = handler;
            this.notificationService = notificationService;
        }

        [Fact]
        public async Task Should_Execute_Successfully()
        {
            var envVariable = new EnvVariable
            {
                Key = "GetEnvVariableHandlerTestsSuccess",
                Value = "VALUE"
            };
            envVariableRepository.Add(envVariable);

            var request = new GetEnvRequest
            {
                Key = "GetEnvVariableHandlerTestsSuccess"
            };

            await handler.ProcessRequest(request);

            request.Logs.Should().NotBeNullOrEmpty();
            request.EnvVariable.Should().NotBeNull();
            request.EnvVariable!.Key.Should().Be("GetEnvVariableHandlerTestsSuccess");
            request.EnvVariable.Value.Should().Be("VALUE");
        }

        [Fact]
        public async Task Should_Add_Notification_If_Invalid_Key()
        {
            var request = new GetEnvRequest
            {
                Key = ""
            };

            await handler.ProcessRequest(request);

            request.Logs.Should().NotBeNullOrEmpty();
            notificationService.HasNotifications.Should().BeTrue();
        }
    }
}
