using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Infrastructure.Modules;
using Telegram.BOT.Infrastructure.Service;
using Xunit;
using Xunit.Abstractions;
using Xunit.Frameworks.Autofac;

[assembly: TestFramework("Telegram.BOT.tests.ConfigureTestFramework", "Telegram.BOT.tests")]
namespace Telegram.BOT.tests
{
    public class ConfigureTestFramework : AutofacTestFramework
    {
        public ConfigureTestFramework(IMessageSink diagnosticMessageSink)
           : base(diagnosticMessageSink)
        {
            Environment.SetEnvironmentVariable("DBCONN", null);
        }
        protected override void ConfigureContainer(ContainerBuilder builder)
        {
          builder.RegisterModule(new ApplicationModule());
          builder.RegisterModule(new InfrastructureModule());
        }
    }
}