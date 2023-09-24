using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Telegram.BOT.Application.Interfaces.Repositories
{
    public interface IMarcRepository
    {
        Domain.Products.Marc Add(Domain.Products.Marc category);
        List<Domain.Products.Marc> GetByFilter(Expression<Func<Domain.Products.Marc, bool>> expression, int page, int pageSize);
        int Update(Domain.Products.Marc category);
        bool Delete(Guid id);
    }
}