using System;
using Telegram.BOT.Domain.Products;

namespace Telegram.BOT.tests.Builder.Domain
{
  public class ProductGroupsBuilder
  {
    private Guid _id = Guid.NewGuid();
    private Guid _product75Id = Guid.NewGuid();
    private BOT.Domain.Products.Product? _product75 = null;
    private Guid _product50Id = Guid.NewGuid();
    private BOT.Domain.Products.Product? _product50 = null;
    private Guid _product25Id = Guid.NewGuid();
    private BOT.Domain.Products.Product? _product25 = null;
    private Guid _groupId = Guid.NewGuid();
    private BOT.Domain.Products.Groups? _group = null;
    public static ProductGroupsBuilder New(BOT.Domain.Products.Product product, BOT.Domain.Products.Groups group)
    {
      return new ProductGroupsBuilder()
      {
        _product75Id = product.Id,
        _product75=product,
        _product50= product,
        _product50Id=product.Id,
        _product25=product,
        _product25Id=product.Id,
        _group=group,
        _groupId=group.Id
      };
    }
    public ProductGroups Build() =>
      new ProductGroups
      {
        Id = _id,
        Product75Id = _product75Id,
        Product75 = _product75,
        Product50Id = _product50Id,
        Product50 = _product50,
        Product25Id = _product25Id,
        Product25 = _product25,
        GroupId = _groupId,
        Group = _group!
      };

    public ProductGroupsBuilder WithId(Guid value)
    {
      _id = value;
      return this;
    }

    public ProductGroupsBuilder WithProduct75Id(Guid value)
    {
      _product75Id = value;
      return this;
    }

    public ProductGroupsBuilder WithProduct75(BOT.Domain.Products.Product? value)
    {
      _product75 = value;
      return this;
    }

    public ProductGroupsBuilder WithProduct50Id(Guid value)
    {
      _product50Id = value;
      return this;
    }

    public ProductGroupsBuilder WithProduct50(BOT.Domain.Products.Product? value)
    {
      _product50 = value;
      return this;
    }

    public ProductGroupsBuilder WithProduct25Id(Guid value)
    {
      _product25Id = value;
      return this;
    }

    public ProductGroupsBuilder WithProduct25(BOT.Domain.Products.Product? value)
    {
      _product25 = value;
      return this;
    }

    public ProductGroupsBuilder WithGroupId(Guid value)
    {
      _groupId = value;
      return this;
    }

    public ProductGroupsBuilder WithGroup(BOT.Domain.Products.Groups value)
    {
      _group = value;
      return this;
    }
  }
}
