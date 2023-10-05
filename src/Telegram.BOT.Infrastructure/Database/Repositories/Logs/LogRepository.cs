using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Domain.Logs;

namespace Telegram.BOT.Infrastructure.Database.Repositories.Logs
{
    public class LogRepository : ILogRepository
    {
        private readonly IMapper mapper;
        private readonly Context context;
        public LogRepository
            (IMapper mapper,
            Context context) 
        {
            this.mapper = mapper;
            this.context = context;
        }
        public int AddRange(List<Log> logs)
        {
            context.logs.AddRange(mapper.Map<List<Entities.Logs.Log>>(logs));
            return context.SaveChanges();
        }
        public List<Log> GetByFilter(Expression<Func<Log, bool>> expression) 
        {
            var predicate = mapper.Map<Expression<Func<Entities.Logs.Log, bool>>>(expression);
            return mapper.Map<List<Log>>(context.logs.Where(predicate).ToList());
        }
        public int RemoveRange(List<Log> logs)
        {
            List<Guid> ids = logs.Select(e => e.Id).ToList();
            var entities = context.logs.Where(e => ids.Contains(e.Id));
            context.logs.RemoveRange(entities);
            return context.SaveChanges();
        }
    }
}
