using System;
using Telegram.BOT.Domain.Products;
namespace Telegram.BOT.tests.Builder.Domain.Product
{
  public  class ProductBuilder
  {
    private Guid id = Guid.NewGuid();
    private string name = "Produto de Teste";
    private string description = "asdasdasdasdasdas";
    private string image = "dasdasdasdas.png";
    private string tags = "Bonito Barato Caro Teste Promoção produto";
    private DateTime createDate = DateTime.Now;
    private List<BOT.Domain.Products.Groups> group75 = new List<BOT.Domain.Products.Groups>();
    private List<BOT.Domain.Products.Groups> group50 = new List<BOT.Domain.Products.Groups>();
    private List<BOT.Domain.Products.Groups> group25 = new List<BOT.Domain.Products.Groups>();
        public static ProductBuilder New(){ return new ProductBuilder(); }
    public BOT.Domain.Products.Product Build() =>
      new(id, name, description, image, tags, createDate) 
      { 
          Group75 = group75, 
          Group50 = group50, 
          Group25 = group25
      };

    public ProductBuilder WithId(Guid value)
    {
      id = value;
      return this;
    }

    public ProductBuilder WithName(string value)
    {
      name = value;
      return this;
    }

    public ProductBuilder WithDescription(string value)
    {
      description = value;
      return this;
    }

    public ProductBuilder WithImage(string value)
    {
      image = value;
      return this;
    }

    public ProductBuilder WithTags(string value)
    {
      tags = value;
      return this;
    }

    public ProductBuilder WithCreateDate(DateTime value)
    {
      createDate = value;
      return this;
    }

    public ProductBuilder WithGroup75(List<BOT.Domain.Products.Groups> value)
    {
      group75 = value;
      return this;
    }

    public ProductBuilder WithGroup50(List<BOT.Domain.Products.Groups> value)
    {
      group50 = value;
      return this;
    }

    public ProductBuilder WithGroup25(List<BOT.Domain.Products.Groups> value)
    {
      group25 = value;
      return this;
    }
  }
}
