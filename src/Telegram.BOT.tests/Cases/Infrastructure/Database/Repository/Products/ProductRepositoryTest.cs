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
    public class ProductRepositoryTest
    {
        private readonly IProductRepository productRepository;
        public ProductRepositoryTest(IProductRepository productRepository)
        {
            this.productRepository=productRepository;
        }
        [Fact]
        public void ShouldSucessAdd()
        {
            var entity = ProductBuilder.New().Build();
            productRepository.Add(entity).Should().NotBeNull();
        }
        [Fact]
        public void ShouldSucessGetByFilter()
        {
            var entity = ProductBuilder.New().Build();
            productRepository.Add(entity);
            productRepository.GetByFilter(e=>e.Id==entity.Id).Should().NotBeNull();
        }
        [Fact]
        public void ShouldSucessUpdate()
        {
            var entity = ProductBuilder.New().Build();
            productRepository.Add(entity);
            var newEntity = ProductBuilder.New().WithId(entity.Id).Build();
            productRepository.Update(newEntity).Should().Be(1);
            var entityNewAttributes = productRepository.GetByFilter(e=>e.Id==entity.Id).First();
            entityNewAttributes.CreateDate.Should().NotBe(entity.CreateDate);
        }
        [Fact]
        public void ShouldSucessRemove()
        {
            var entity = ProductBuilder.New().Build();
            productRepository.Add(entity);
            productRepository.Delete(entity.Id).Should().BeTrue();
            productRepository.Delete(Guid.NewGuid()).Should().BeFalse();
        }
    }
}