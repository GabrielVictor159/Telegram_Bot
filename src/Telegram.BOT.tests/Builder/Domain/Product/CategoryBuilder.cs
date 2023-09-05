using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Products;

namespace Telegram.BOT.tests.Builder.Domain.Product
{
    public class CategoryBuilder
    {
    private Guid id = Guid.NewGuid();
    private string name = "Name Test";
    private List<Marc> marcs = new List<Marc>();
    public static CategoryBuilder New()
    {
      return new CategoryBuilder();
    }
    public Category Build() =>
      new Category(id,name,marcs);

    public CategoryBuilder WithId(Guid value)
    {
      id = value;
      return this;
    }

    public CategoryBuilder WithName(string value)
    {
      name = value;
      return this;
    }

    public CategoryBuilder Withmarcs(List<Marc> value)
    {
      marcs = value;
      return this;
    }
    }
}