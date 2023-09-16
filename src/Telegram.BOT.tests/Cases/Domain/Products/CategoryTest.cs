using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;

namespace Telegram.BOT.tests.Cases.Domain.Products
{
    public class CategoryTest
    {
        [Fact]
        public void ShouldCategoryBeValid()
        {
            var Category = CategoryBuilder.New().Build();
            Category.IsValid.Should().BeTrue();
        }
        [Fact]
        public void ShouldFailIfIdIsEmpty()
        {
           var Category = CategoryBuilder.New().WithId(Guid.Empty).Build();
            Category.IsValid.Should().BeFalse(); 
        }
        [Fact]
        public void ShouldFailIfIdNameIsLessThan5Characters()
        {
           var Category = CategoryBuilder.New().WithName("a").Build();
            Category.IsValid.Should().BeFalse(); 
        }
    }
}