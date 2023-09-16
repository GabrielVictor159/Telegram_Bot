using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Products;

namespace Telegram.BOT.Application.Interfaces.Repositories
{
    public interface IProductGroupsRepository
    {
        ProductGroups Add(ProductGroups ProductGroup);
        List<ProductGroups> AddRange (List<ProductGroups> ProductGroups);
        ProductGroups GetOne(Guid id);
        List<ProductGroups> GetByFilter(Expression<Func<ProductGroups, bool>> expression);
        int Remove(ProductGroups ProductGroup);
        int RemoveRange(List<ProductGroups> ProductGroups);
    }
}