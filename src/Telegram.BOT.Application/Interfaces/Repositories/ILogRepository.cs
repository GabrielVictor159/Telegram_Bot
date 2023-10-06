using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Logs;

namespace Telegram.BOT.Application.Interfaces.Repositories
{
    public interface ILogRepository
    {
        int AddRange(List<Log> logs);
        List<Log> GetByFilter(Expression<Func<Log, bool>> expression);
        int RemoveRange(List<Log> logs);
    }
}
