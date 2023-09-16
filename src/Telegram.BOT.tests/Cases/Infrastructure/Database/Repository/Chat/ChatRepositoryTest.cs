using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.tests.Builder.Domain.Chat;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Infrastructure.Database.Repository.Chat
{
    [UseAutofacTestFramework]
    public class ChatRepositoryTest
    {
        private readonly IChatRepository chatRepository;
        public ChatRepositoryTest(IChatRepository chatRepository)
        {
            this.chatRepository=chatRepository;
        }
        [Fact]
        public void ShouldSucessAdd()
        {
            var entity = ChatBuilder.New().Build();
            chatRepository.Add(entity).Should().NotBeNull();
        }
        [Fact]
        public void ShouldSucessAddRange()
        {
            var entities = new List<BOT.Domain.Chat.Chat>()
            {
                ChatBuilder.New().Build(),
            };
            chatRepository.AddRange(entities).Should().NotBeNull();
        }
        [Fact]
        public void ShouldSucessGetOne()
        {
            var entity = ChatBuilder.New().Build();
            chatRepository.Add(entity);
            chatRepository.GetOne(entity.Id).Should().NotBeNull();
        }
        [Fact]
        public void ShouldSucessGetByFilter()
        {
            var entity = ChatBuilder.New().Build();
            chatRepository.Add(entity);
            chatRepository.GetByFilter(e=>e.Id==entity.Id).Should().NotBeNull();
        }
        [Fact]
        public void ShouldSucessRemove()
        {
            var entity = ChatBuilder.New().Build();
            chatRepository.Add(entity);
            chatRepository.Remove(entity).Should().Be(1);
        }
        [Fact]
        public void ShouldSucessRemoveRange()
        {
            var entities = new List<BOT.Domain.Chat.Chat>()
            {
                ChatBuilder.New().Build()
            };
            chatRepository.AddRange(entities);
            chatRepository.RemoveRange(entities).Should().Be(entities.Count);
        }
    }
}