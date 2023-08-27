using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.BOT.Infrastructure.Database.Entities;

namespace Telegram.BOT.Infrastructure.Database.AutoMapperProfile
{
    public class InfraDomainProfile : AutoMapper.Profile
    {
        public InfraDomainProfile()
        {
            CreateMap<Order, Domain.Order.Order>().ReverseMap();
        }
    }
}