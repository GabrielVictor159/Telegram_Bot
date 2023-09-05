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
            CreateMap<Marc, Domain.Products.Marc>().ReverseMap();
            CreateMap<Category,Domain.Products.Category>().ReverseMap();
            CreateMap<Product, Domain.Products.Product>().ReverseMap();
            CreateMap<ProductGroups, Domain.Products.ProductGroups>().ReverseMap();
            CreateMap<Groups, Domain.Products.Groups>().ReverseMap();
        }
    }
}