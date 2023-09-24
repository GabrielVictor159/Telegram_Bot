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
    public class MarcRepositoryTest
    {
        private readonly IMarcRepository marcRepository;
        public MarcRepositoryTest(IMarcRepository marcRepository)
        {
            this.marcRepository = marcRepository;
        }
        [Fact]
        public void ShouldSucessAdd()
        {
            var entity = MarcBuilder.New().Build();
            marcRepository.Add(entity).Should().NotBeNull();
        }
        [Fact]
        public void ShouldSucessGetByFilter()
        {
            var entity = MarcBuilder.New().Build();
            marcRepository.Add(entity);
            marcRepository.GetByFilter(e=>e.Id==entity.Id,1,10).Should().NotBeNull();
        }
        [Fact]
        public void ShouldSucessUpdate()
        {
            var entity = MarcBuilder.New().Build();
            marcRepository.Add(entity);
            var newEntity = MarcBuilder.New().WithId(entity.Id).WithName("dasdasasda").Build();
            marcRepository.Update(newEntity).Should().Be(1);
            var entityNewAttributes = marcRepository.GetByFilter(e=>e.Id==entity.Id,1,10).First();
            entityNewAttributes.Name.Should().NotBe(entity.Name);
        }
        [Fact]
        public void ShouldSucessRemove()
        {
            var entity = MarcBuilder.New().Build();
            marcRepository.Add(entity);
            marcRepository.Delete(entity.Id).Should().BeTrue();
            marcRepository.Delete(Guid.NewGuid()).Should().BeFalse();
        }
    }
}