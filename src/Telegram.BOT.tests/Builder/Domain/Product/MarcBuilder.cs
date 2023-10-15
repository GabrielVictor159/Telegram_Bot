using Bogus;
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
        private Category? category = CategoryBuilder.New().Build();
        private List<BOT.Domain.Products.Product> products = new List<BOT.Domain.Products.Product>();
        public static MarcBuilder New()
        {
            var faker = new Faker("pt_BR");
            return new MarcBuilder() { name = faker.Random.String2(faker.Random.Int(8,20)) };
        }
        public Marc Build() =>
          new Marc()
          {
              Id = id,
              Name = name,
              CategoryId = category!.Id,
              Category=category,products=products
          };
    
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