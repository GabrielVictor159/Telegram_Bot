using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Telegram.BOT.tests.Builder.Domain.Chat;
using Xunit;

namespace Telegram.BOT.tests.Cases.Domain.Chat
{
    public class ChatTest
    {
        [Fact]
        public void ShouldChatBeValid()
        {
            var chat = ChatBuilder.New().Build();
            chat.IsValid.Should().BeTrue();
        }
        [Fact]
        public void ShouldFailIfIdIsEmpty()
        {
            var chat = ChatBuilder.New().WithId(Guid.Empty).Build();
            chat.IsValid.Should().BeFalse();
        }

    }
}