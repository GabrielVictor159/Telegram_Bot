using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.BOT.Application.Interfaces.Repositories
{
    public interface IGroupsRepository
    {
        Domain.Products.Groups Add(Domain.Products.Groups group);
        List<Domain.Products.Groups> AddRange(List<Domain.Products.Groups> groups);
        Domain.Products.Groups? GetOne(Guid id);
        List<Domain.Products.Groups> GetByFilter(Expression<Func<Domain.Products.Groups, bool>> expression);
        List<Domain.Products.Groups> GetByLeveinsthein(string s1, double percentagem);
        bool Remove(Guid id);

    }
}
