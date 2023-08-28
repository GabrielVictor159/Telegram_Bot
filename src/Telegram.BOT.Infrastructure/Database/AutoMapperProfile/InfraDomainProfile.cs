using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.BOT.Infrastructure.Database.Entities;
using Telegram.BOT.Infrastructure.Database.Entities.Products;

namespace Telegram.BOT.Infrastructure.Database.AutoMapperProfile
{
    public class InfraDomainProfile : AutoMapper.Profile
    {
        public InfraDomainProfile()
        {
            CreateMap<Order, Domain.Order.Order>().ReverseMap();
            CreateMap<Groups, Domain.Products.Groups>().ReverseMap();
        }
    }
}