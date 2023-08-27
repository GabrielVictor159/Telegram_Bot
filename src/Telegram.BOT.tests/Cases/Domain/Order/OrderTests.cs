using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Telegram.BOT.tests.Builder.Domain;
using Xunit;
namespace Telegram.BOT.tests.Cases.Domain.Order
{
    public class OrderTests
    {
        [Fact]
        public void ShouldCreateDomaminWithSucces()
        {
            var domain = OrderBuilder.New().Build();
            domain.IsValid.Should().BeTrue();
        }
        [Fact]
        public void ShouldNotBeGuidEmpty()
        {
            var domain = OrderBuilder.New().WithId(Guid.Empty).Build();
            domain.IsValid.Should().BeFalse();
            domain.ValidationResult.Should().NotBeNull();
            domain.ValidationResult?.Errors.Should().HaveCountGreaterThan(0);
        }
         [Fact]
        public void ShouldNotBeOrderDateGreaterThanToday()
        {
            var domain = OrderBuilder.New().WithOrderDate(DateTime.UtcNow.AddDays(1)).Build();
            domain.IsValid.Should().BeFalse();
            domain.ValidationResult.Should().NotBeNull();
            domain.ValidationResult?.Errors.Should().HaveCountGreaterThan(0);
        }
         [Fact]
        public void ShouldNotBeTotalOrderLessOrEqualThan0()
        {
            var domain = OrderBuilder.New().WithTotalOrder(0).Build();
            domain.IsValid.Should().BeFalse();
            domain.ValidationResult.Should().NotBeNull();
            domain.ValidationResult?.Errors.Should().HaveCountGreaterThan(0);
        }
    }
}