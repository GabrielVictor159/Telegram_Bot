using System;
using Telegram.BOT.Domain.Products;
namespace Telegram.BOT.tests.Builder.Domain.Product
{
  public class GroupsBuilder
  {
    private Guid id = Guid.NewGuid();
    private string tags = "Bonito Barato Caro Teste Promoção";
    private DateTime createDate = DateTime.Now;
    public static GroupsBuilder New() { return new GroupsBuilder(); }
    public Groups Build() =>
      new Groups()
      { 
      Id = id,
      Tags = tags,
      CreateDate = createDate,
      };

    public GroupsBuilder WithId(Guid value)
    {
      id = value;
      return this;
    }

    public GroupsBuilder WithTags(string value)
    {
      tags = value;
      return this;
    }

    public GroupsBuilder WithCreateDate(DateTime value)
    {
      createDate = value;
      return this;
    }
  }
}
