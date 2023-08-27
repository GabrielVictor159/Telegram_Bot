using FluentAssertions;
using System;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;

namespace Telegram.BOT.tests.Cases.Domain.Groups
{
    public class GroupTest
    {
        [Fact]
        public void ShouldGroupBeValid()
        {
            var group = GroupsBuilder.New().Build();
            group.IsValid.Should().BeTrue();
        }

        [Fact]
        public void ShouldFailIfIdIsEmpty()
        {
            var group = GroupsBuilder.New().WithId(Guid.Empty).Build();
            group.IsValid.Should().BeFalse();
        }

        [Fact]
        public void ShouldFailIfTagsIsEmpty()
        {
            var group = GroupsBuilder.New().WithTags("").Build();
            group.IsValid.Should().BeFalse();
        }

    }
}
