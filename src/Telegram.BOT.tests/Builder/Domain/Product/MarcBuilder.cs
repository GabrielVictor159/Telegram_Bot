using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Products;

namespace Telegram.BOT.tests.Builder.Domain.Product
{
    public class MarcBuilder
    {
        private Guid id = Guid.NewGuid();
        private string name = "Name Test";
        private Category? category = null;
        private List<BOT.Domain.Products.Product> products = new List<BOT.Domain.Products.Product>();
        public static MarcBuilder New()
        {
          return new MarcBuilder();
        }
        public Marc Build() =>
          new Marc(id, name, products, category);
    
        public MarcBuilder WithId(Guid value)
        {
          id = value;
          return this;
        }
    
        public MarcBuilder WithName(string value)
        {
          name = value;
          return this;
        }
    
        public MarcBuilder WithCategory(Category? value)
        {
          category = value;
          return this;
        }
    
        public MarcBuilder WithProducts(List<BOT.Domain.Products.Product> value)
        {
          products = value;
          return this;
        }
    }
}