using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Infrastructure.Database.Repository.Products
{
    [UseAutofacTestFramework]
    public class GroupsRepositoryTest
    {
        private readonly IGroupsRepository groupsRepository;
        public GroupsRepositoryTest(IGroupsRepository groupsRepository)
        {
            this.groupsRepository=groupsRepository;
        }
        [Fact]
        public void ShouldSucessAdd()
        {
            var entity = GroupsBuilder.New().Build();
            groupsRepository.Add(entity).Should().NotBeNull();
        }
        [Fact]
        public void ShouldSucessAddRange()
        {
            var entities = new List<BOT.Domain.Products.Groups>()
            {
                GroupsBuilder.New().Build(),
            };
            groupsRepository.AddRange(entities).Should().NotBeNull();
        }
        [Fact]
        public void ShouldSucessGetOne()
        {
            var entity = GroupsBuilder.New().Build();
            groupsRepository.Add(entity);
            groupsRepository.GetOne(entity.Id).Should().NotBeNull();
        }
        [Fact]
        public void ShouldSucessGetByFilter()
        {
            var entity = GroupsBuilder.New().Build();
            groupsRepository.Add(entity);
            groupsRepository.GetByFilter(e=>e.Id==entity.Id).Should().NotBeNull();
        }
        [Fact]
        public void ShouldSucessRemove()
        {
            var entity = GroupsBuilder.New().Build();
            groupsRepository.Add(entity);
            groupsRepository.Remove(entity.Id).Should().BeTrue();
            groupsRepository.Remove(Guid.NewGuid()).Should().BeFalse();
        }
    }
}