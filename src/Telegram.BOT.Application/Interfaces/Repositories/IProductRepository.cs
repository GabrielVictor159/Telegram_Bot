using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.BOT.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Domain.Products.Product Add(Domain.Products.Product product);
        List<Domain.Products.Product> GetByFilter(Expression<Func<Domain.Products.Product, bool>> expression);
        int Update(Domain.Products.Product product);
        bool Delete(Guid id);
    }
}
