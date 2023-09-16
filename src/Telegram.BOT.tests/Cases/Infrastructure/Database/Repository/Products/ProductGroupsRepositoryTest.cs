using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentAssertions;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.tests.Builder.Domain;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Infrastructure.Database.Repository.Products
{
    [UseAutofacTestFramework]
    public class ProductGroupsRepositoryTest
    {
       private readonly IProductGroupsRepository productGroupsRepository;
        public ProductGroupsRepositoryTest(IProductGroupsRepository productGroupsRepository)
        {
            this.productGroupsRepository=productGroupsRepository;
        }
        [Fact]
        public void ShouldSucessAdd()
        {
            var entity = ProductGroupsBuilder.New().Build();
            productGroupsRepository.Add(entity).Should().NotBeNull();
        }
        [Fact]
        public void ShouldSucessAddRange()
        {
            var entities = new List<BOT.Domain.Products.ProductGroups>()
            {
                ProductGroupsBuilder.New().Build(),
            };
            productGroupsRepository.AddRange(entities).Should().NotBeNull();
        }
        [Fact]
        public void ShouldSucessGetOne()
        {
            var entity = ProductGroupsBuilder.New().Build();
            productGroupsRepository.Add(entity);
            productGroupsRepository.GetOne(entity.Id).Should().NotBeNull();
        }
        [Fact]
        public void ShouldSucessGetByFilter()
        {
            var entity = ProductGroupsBuilder.New().Build();
            productGroupsRepository.Add(entity);
            productGroupsRepository.GetByFilter(e=>e.Id==entity.Id).Should().NotBeNull();
        }
        [Fact]
        public void ShouldSucessRemove()
        {
            var entity = ProductGroupsBuilder.New().Build();
            productGroupsRepository.Add(entity);
            productGroupsRepository.Remove(entity).Should().Be(1);
        }
        [Fact]
        public void ShouldSucessRemoveRange()
        {
            var entities = new List<BOT.Domain.Products.ProductGroups>()
            {
                ProductGroupsBuilder.New().Build()
            };
            productGroupsRepository.AddRange(entities);
            productGroupsRepository.RemoveRange(entities).Should().Be(entities.Count);
        } 
    }
}