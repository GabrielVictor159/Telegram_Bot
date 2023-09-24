using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Telegram.BOT.Application.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Domain.Products.Category Add(Domain.Products.Category category);
        List<Domain.Products.Category> GetByFilter(Expression<Func<Domain.Products.Category, bool>> expression, int page, int pageSize);
        int Update(Domain.Products.Category category);
        bool Delete(Guid id);
    }
}