using System;
using Telegram.BOT.Domain.Products;
using Telegram.BOT.tests.Builder.Domain.Product;

namespace Telegram.BOT.tests.Builder.Domain
{
  public class ProductGroupsBuilder
  {
    public Guid id {get;private set;}
    public Guid productId {get;private set;}
    public BOT.Domain.Products.Product? product {get;private set;}
    public Guid groupId {get;private set;}
    public BOT.Domain.Products.Groups? group {get;private set;}
    public double Percentagem { get;private set;}
    public static ProductGroupsBuilder New()
    {
      var group = GroupsBuilder.New().Build();
      var product = ProductBuilder.New().Build();
            return new ProductGroupsBuilder()
            {
                id = Guid.NewGuid(),
                productId = product.Id,
                product = product,
                group = group,
                groupId = group.Id,
                Percentagem = 0.8
      };
    }
    public ProductGroups Build() =>
      new ProductGroups
      {
        Id = id,
        ProductId = productId,
        Product = product,
        GroupId = groupId,
        Group = group!,
        Percentage = Percentagem
      };

    public ProductGroupsBuilder WithId(Guid value)
    {
      id = value;
      return this;
    }

    public ProductGroupsBuilder WithProductId(Guid value)
    {
      productId = value;
      return this;
    }

    public ProductGroupsBuilder WithProduct(BOT.Domain.Products.Product? value)
    {
      product = value;
      productId = value!.Id;
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
    public ProductGroupsBuilder WithPercentagem(double value)
    {
      Percentagem = value;
      return this;
    }
  }
}
