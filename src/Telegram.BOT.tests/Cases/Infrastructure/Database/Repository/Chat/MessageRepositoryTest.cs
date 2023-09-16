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
    public class MessageRepositoryTest
    {
        private readonly IMessageRepository messageRepository;
        public MessageRepositoryTest(IMessageRepository messageRepository)
        {
            this.messageRepository=messageRepository;
        }
        [Fact]
        public void ShouldSucessAdd()
        {
            var entity = MessageBuilder.New().Build();
            messageRepository.Add(entity).Should().NotBeNull();
        }
        [Fact]
        public void ShouldSucessAddRange()
        {
            var entities = new List<BOT.Domain.Chat.Message>()
            {
                MessageBuilder.New().Build(),
            };
            messageRepository.AddRange(entities).Should().NotBeNull();
        }
        [Fact]
        public void ShouldSucessGetOne()
        {
            var entity = MessageBuilder.New().Build();
            messageRepository.Add(entity);
            messageRepository.GetOne(entity.Id).Should().NotBeNull();
        }
        [Fact]
        public void ShouldSucessGetByFilter()
        {
            var entity = MessageBuilder.New().Build();
            messageRepository.Add(entity);
            messageRepository.GetByFilter(e=>e.Id==entity.Id).Should().NotBeNull();
        }
        [Fact]
        public void ShouldSucessRemove()
        {
            var entity = MessageBuilder.New().Build();
            messageRepository.Add(entity);
            messageRepository.Remove(entity).Should().Be(1);
        }
        [Fact]
        public void ShouldSucessRemoveRange()
        {
            var entities = new List<BOT.Domain.Chat.Message>()
            {
                MessageBuilder.New().Build()
            };
            messageRepository.AddRange(entities);
            messageRepository.RemoveRange(entities).Should().Be(entities.Count);
        }
    }
}