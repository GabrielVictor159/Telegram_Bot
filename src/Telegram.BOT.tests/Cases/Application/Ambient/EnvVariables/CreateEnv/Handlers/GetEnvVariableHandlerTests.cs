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
using Telegram.BOT.Application.Interfaces.Services;

namespace Telegram.BOT.tests.Cases.Application.Ambient.EnvVariables.CreateEnv.Handlers
{
    [UseAutofacTestFramework]
    public class GetEnvVariableHandlerTests
    {
        private readonly IEnvVariableRepository envVariableRepository;
        private readonly GetEnvVariableHandler handler;
        private readonly INotificationService notficationService;

        public GetEnvVariableHandlerTests(IEnvVariableRepository envVariableRepository, GetEnvVariableHandler handler, INotificationService notficationService)
        {
            this.envVariableRepository = envVariableRepository;
            this.handler = handler;
            this.notficationService = notficationService;
        }

        [Fact]
        public async Task Should_Return_Notification_If_Exists()
        {
            var envVariable = new EnvVariable { Key = "GetEnvVariableHandlerTests", Value = "VALUE" };
            envVariableRepository.Add(envVariable);
            var request = new CreateEnvRequest
            {
                EnvVariable = new EnvVariable { Key = "GetEnvVariableHandlerTests", Value = "VALUE" }
            };
            await handler.ProcessRequest(request);
            request.Logs.Should().NotBeNullOrEmpty();
            notficationService.HasNotifications.Should().BeTrue();
        }
    }
}
