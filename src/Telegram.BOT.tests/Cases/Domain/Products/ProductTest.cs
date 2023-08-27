using FluentAssertions;
using System;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;

namespace Telegram.BOT.tests.Cases.Domain.Products
{
    public class ProductTest
    {
        [Fact]
        public void ShouldProductBeValid()
        {
            var product = ProductBuilder.New().Build();
            product.IsValid.Should().BeTrue();
        }

        [Fact]
        public void ShouldFailIfIdIsEmpty()
        {
            var product = ProductBuilder.New().WithId(Guid.Empty).Build();
            product.IsValid.Should().BeFalse();
        }

        [Fact]
        public void ShouldFailIfNameIsEmpty()
        {
            var product = ProductBuilder.New().WithName("").Build();
            product.IsValid.Should().BeFalse();
        }

        [Fact]
        public void ShouldFailIfTagsIsLessThan5Words()
        {
            var product = ProductBuilder.New().WithTags("tag1 tag2 tag3").Build();
            product.IsValid.Should().BeFalse();
        }
    }
}
