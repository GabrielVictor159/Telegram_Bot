using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
