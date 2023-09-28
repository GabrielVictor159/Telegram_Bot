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
    private double price = 50;
    private Marc? marc = MarcBuilder.New().Build();
    private List<ProductGroups> groups = new List<ProductGroups>();
    private Groups? _group = GroupsBuilder.New().Build();
        public static ProductBuilder New()
        { 
           return new ProductBuilder(); 
        }
    public BOT.Domain.Products.Product Build()
    {
     BOT.Domain.Products.Product produt = new()
     {
         Id = id,
         Name = name,
         Description = description,
         Image = image,
         Tags = tags,
         CreateDate = createDate,
         Price = price,
         MarcId = marc!.Id,
         Marc = marc,
         Groups = groups
     };
     return produt;
    }
    public ProductBuilder WithMarc(Marc value)
    {
      marc = value;
      return this;
    }
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
    public ProductBuilder WithPrice(double value)
    {
      price = value;
      return this;
    }

    public ProductBuilder WithGroups(List<ProductGroups> value)
    {
      groups = value;
      return this;
    }
    public ProductBuilder WithGroup(Groups value)
    {
      _group = value;
      return this;
    }

  }
}
