using System;
using Telegram.BOT.Domain.Products;
using Telegram.BOT.tests.Builder.Domain.Product;

namespace Telegram.BOT.tests.Builder.Domain
{
  public class ProductGroupsBuilder
  {
    public Guid id {get;private set;}
    public Guid product75Id {get;private set;}
    public BOT.Domain.Products.Product? product75 {get;private set;}
    public Guid product50Id {get;private set;}
    public BOT.Domain.Products.Product? product50 {get;private set;}
    public Guid product25Id {get;private set;}
    public BOT.Domain.Products.Product? product25 {get;private set;}
    public Guid groupId {get;private set;}
    public BOT.Domain.Products.Groups? group {get;private set;}
    public static ProductGroupsBuilder New()
    {
      var group = GroupsBuilder.New().Build();
      var product1 = ProductBuilder.New().Build();
      var product2 = ProductBuilder.New().Build();
      var product3 = ProductBuilder.New().Build();
      return new ProductGroupsBuilder()
      {
        id = Guid.NewGuid(),
        product75Id = product3.Id,
        product75=product3,
        product50= product2,
        product50Id=product2.Id,
        product25=product1,
        product25Id=product1.Id,
        group=group,
        groupId=group.Id
      };
    }
    public ProductGroups Build() =>
      new ProductGroups
      {
        Id = id,
        Product75Id = product75Id,
        Product75 = product75,
        Product50Id = product50Id,
        Product50 = product50,
        Product25Id = product25Id,
        Product25 = product25,
        GroupId = groupId,
        Group = group!
      };

    public ProductGroupsBuilder WithId(Guid value)
    {
      id = value;
      return this;
    }

    public ProductGroupsBuilder WithProduct75Id(Guid value)
    {
      product75Id = value;
      return this;
    }

    public ProductGroupsBuilder WithProduct75(BOT.Domain.Products.Product? value)
    {
      product75 = value;
      product75Id = value!.Id;
      return this;
    }

    public ProductGroupsBuilder WithProduct50Id(Guid value)
    {
      product50Id = value;
      return this;
    }

    public ProductGroupsBuilder WithProduct50(BOT.Domain.Products.Product? value)
    {
      product50 = value;
      product50Id = value!.Id;
      return this;
    }

    public ProductGroupsBuilder WithProduct25Id(Guid value)
    {
      product25Id = value;
      return this;
    }

    public ProductGroupsBuilder WithProduct25(BOT.Domain.Products.Product? value)
    {
      product25 = value;
      product25Id = value!.Id;
      return this;
    }

    public ProductGroupsBuilder WithGroupId(Guid value)
    {
      groupId = value;
      return this;
    }

    public ProductGroupsBuilder WithGroup(BOT.Domain.Products.Groups value)
    {
      group = value;
      groupId = value.Id;
      return this;
    }
  }
}
