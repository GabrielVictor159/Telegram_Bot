using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Infrastructure.Service;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Infrastructure.Services
{
    [UseAutofacTestFramework]
    public class ProbabilityOperationsTest
    {
        [Fact]
        public void ShouldPercentagemStrings()
        {
            var str = "sssssssssssssssssssssssssssss";
            var newStr = str.Substring(0, ((int)(str.Length*0.9)));
            var result = ProbabilityOperations.CalculateNormalizedLevenshteinDistance(str, newStr);
            result.Should().BeGreaterThan(0.8);
        }
    }
}
