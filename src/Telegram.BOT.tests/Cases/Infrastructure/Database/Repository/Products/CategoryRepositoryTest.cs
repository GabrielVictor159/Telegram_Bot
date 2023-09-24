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
    public class CategoryRepositoryTest
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryRepositoryTest(ICategoryRepository categoryRepository)
        {
            this.categoryRepository=categoryRepository;
        }
        [Fact]
        public void ShouldSucessAdd()
        {
            var entity = CategoryBuilder.New().Build();
            categoryRepository.Add(entity).Should().NotBeNull();
        }
        [Fact]
        public void ShouldSucessGetByFilter()
        {
            var entity = CategoryBuilder.New().Build();
            categoryRepository.Add(entity);
            categoryRepository.GetByFilter(e=>e.Id==entity.Id,1,10).Should().NotBeNull();
        }
        [Fact]
        public void ShouldSucessUpdate()
        {
            var entity = CategoryBuilder.New().Build();
            categoryRepository.Add(entity);
            var newEntity = CategoryBuilder.New().WithId(entity.Id).WithName("dasdasasda").Build();
            categoryRepository.Update(newEntity).Should().Be(1);
            var entityNewAttributes = categoryRepository.GetByFilter(e=>e.Id==entity.Id,1,10).First();
            entityNewAttributes.Name.Should().NotBe(entity.Name);
        }
        [Fact]
        public void ShouldSucessRemove()
        {
            var entity = CategoryBuilder.New().Build();
            categoryRepository.Add(entity);
            categoryRepository.Delete(entity.Id).Should().BeTrue();
            categoryRepository.Delete(Guid.NewGuid()).Should().BeFalse();
        }
    }
}