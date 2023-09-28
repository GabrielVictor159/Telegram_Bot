using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Products.CreateProduct;
using Telegram.BOT.Application.UseCases.Products.CreateProduct.Handlers;
using Telegram.BOT.Domain.Products;
using Telegram.BOT.Infrastructure.Database.Repositories.Products;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Application.Products.CreateProduct.Handlers;

[UseAutofacTestFramework]
public class CreateProductGroupsHandlerTest
{
    private readonly CreateProductGroupsHandler createProductGroupsHandler;
    private readonly IProductGroupsRepository productGroupsRepository;
    private readonly IGroupsRepository groupsRepository;
    private readonly IProductRepository productRepository;
    public CreateProductGroupsHandlerTest
        (CreateProductGroupsHandler createProductGroupsHandler,
        IProductGroupsRepository productGroupsRepository,
        IGroupsRepository groupsRepository,
        IProductRepository productRepository)
    {
        this.createProductGroupsHandler = createProductGroupsHandler;
        this.productGroupsRepository = productGroupsRepository;
        this.groupsRepository = groupsRepository;
        this.productRepository = productRepository;
    }
    [Fact]
    public async Task ShouldSucess()
    {
        var product = ProductBuilder.New().Build();
        productRepository.Add(product);
        var request = new CreateProductRequest(){ Product = product, Image = new byte[10] };
        var groups = new List<Groups>();
        groups.Add(GroupsBuilder.New()
            .WithTags(product.Tags.Substring(0, (int)(product.Tags.Length * 0.80))).Build());
        groups.Add(GroupsBuilder.New()
            .WithTags(product.Tags.Substring(0, (int)(product.Tags.Length * 0.60))).Build());
        groups.Add(GroupsBuilder.New()
            .WithTags(product.Tags.Substring(0, (int)(product.Tags.Length * 0.40))).Build());
        groupsRepository.AddRange(groups);
        await createProductGroupsHandler.ProcessRequest(request);
        var resultDb = productGroupsRepository.GetByFilter(e => 
        e.ProductId == product.Id).ToList();
        resultDb.Count.Should().BeGreaterThan(4);
    }
}
