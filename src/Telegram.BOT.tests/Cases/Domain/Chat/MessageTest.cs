using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Telegram.BOT.tests.Builder.Domain.Chat;
using Xunit;

namespace Telegram.BOT.tests.Cases.Domain.Chat
{
    public class MessageTest
    {
        [Fact]
        public void ShouldChatBeValid()
        {
            var message = MessageBuilder.New().Build();
            message.IsValid.Should().BeTrue();
        }
        [Fact]
        public void ShouldFailIfIdIsEmpty()
        {
            var message = MessageBuilder.New().WithId(Guid.Empty).Build();
            message.IsValid.Should().BeFalse();
        }
        [Fact]
        public void ShouldFailIfMessagingIsEmpty()
        {
            var message = MessageBuilder.New().WithMessaging("").Build();
            message.IsValid.Should().BeFalse();
        }
    }
}