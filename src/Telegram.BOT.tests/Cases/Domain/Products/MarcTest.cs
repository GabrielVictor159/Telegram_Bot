using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;

namespace Telegram.BOT.tests.Cases.Domain.Products
{
    public class MarcTest
    {
        [Fact]
        public void ShouldMarcBeValid()
        {
            var marc = MarcBuilder.New().Build();
            marc.IsValid.Should().BeTrue();
        }
        [Fact]
        public void ShouldldFailIfIdIsEmpty()
        {
            var marc = MarcBuilder.New().WithId(Guid.Empty).Build();
            marc.IsValid.Should().BeFalse();
        }
        [Fact]
        public void ShouldFailIfIdNameIsLessThan5Characters()
        {
           var marc = MarcBuilder.New().WithName("a").Build();
            marc.IsValid.Should().BeFalse(); 
        }
    }
}