using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.BOT.Infrastructure.Database.Entities;
using Telegram.BOT.Infrastructure.Database.Entities.Chat;
using Telegram.BOT.Infrastructure.Database.Entities.Logs;
using Telegram.BOT.Infrastructure.Database.Entities.Products;

namespace Telegram.BOT.Infrastructure.Database.AutoMapperProfile
{
    public class InfraDomainProfile : AutoMapper.Profile
    {
        public InfraDomainProfile()
        {
            CreateMap<Log, Domain.Logs.Log>().ReverseMap();
            CreateMap<Marc, Domain.Products.Marc>().ReverseMap();
            CreateMap<Category,Domain.Products.Category>().ReverseMap();
            CreateMap<Product, Domain.Products.Product>().ReverseMap();
            CreateMap<ProductGroups, Domain.Products.ProductGroups>().ReverseMap();
            CreateMap<Groups, Domain.Products.Groups>().ReverseMap();
            CreateMap<ProductGroups, Domain.Products.ProductGroups>()
            .ForMember(dest=>dest.Product, opt => opt.MapFrom(src=>src.Product))
            .ForMember(dest=>dest.Group, opt => opt.MapFrom(src=>src.Group))
            .ReverseMap();
            CreateMap<Product, Domain.Products.Product>()
            .ForMember(dest=>dest.Groups, opt => opt.MapFrom(src=>src.Groups))
            .ReverseMap();
            CreateMap<Chat, Domain.Chat.Chat>().ReverseMap();
            CreateMap<Message, Domain.Chat.Message>()
            .ForMember(dest=>dest.Chat, opt => opt.MapFrom(src=>src.Chat))
            .ReverseMap();
        }
    }
}