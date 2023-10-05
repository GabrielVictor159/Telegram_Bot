using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.CreateEnv.Handlers;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.CreateEnv;
using Telegram.BOT.Domain.Enums;
using Xunit;
using Xunit.Frameworks.Autofac;
using FluentAssertions;
using Telegram.BOT.Domain.Logs;
using ManagementServices.variables.Domain.Models;

namespace Telegram.BOT.tests.Cases.Application.Ambient.EnvVariables.CreateEnv.Handlers
{
    [UseAutofacTestFramework]
    public class ValidateEnvVariableHandlerTests
    {
        private readonly INotificationService notificationService;
        private readonly ValidateEnvVariableHandler handler;

        public ValidateEnvVariableHandlerTests
            (INotificationService notificationService, 
            ValidateEnvVariableHandler handler)
        {
            this.notificationService = notificationService;
            this.handler = handler;
        }

        [Fact]
        public async Task Should_Validate_EnvVariable_Successfully()
        {
            var request = new CreateEnvRequest
            {
                EnvVariable = new EnvVariable { Key = "ValidateEnvVariableHandlerTests", Value = "VALUE" }
            };
            await handler.ProcessRequest(request);
            request.Logs.Should().NotBeNullOrEmpty();
            notificationService.HasNotifications.Should().BeFalse();
        }

        [Fact]
        public async Task Should_Add_Notifications_If_EnvVariable_Is_Invalid()
        {
            var request = new CreateEnvRequest
            {
                EnvVariable = new EnvVariable { Key = "", Value = "Invalid value" }
            };
            await handler.ProcessRequest(request);
            request.Logs.Should().NotBeNullOrEmpty();
            notificationService.HasNotifications.Should().BeTrue();
        }
    }
}
