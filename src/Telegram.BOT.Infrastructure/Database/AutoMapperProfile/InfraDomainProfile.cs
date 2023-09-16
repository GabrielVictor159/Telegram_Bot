using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.BOT.Infrastructure.Database.Entities;
using Telegram.BOT.Infrastructure.Database.Entities.Chat;
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
            CreateMap<ProductGroups, Domain.Products.ProductGroups>()
            .ForMember(dest=>dest.Product25, opt => opt.MapFrom(src=>src.Product25))
            .ForMember(dest=>dest.Product50, opt => opt.MapFrom(src=>src.Product50))
            .ForMember(dest=>dest.Product75, opt => opt.MapFrom(src=>src.Product75))
            .ForMember(dest=>dest.Group, opt => opt.MapFrom(src=>src.Group))
            .ReverseMap();
            CreateMap<Product, Domain.Products.Product>()
            .ForMember(dest=>dest.Group25, opt => opt.MapFrom(src=>src.Group25))
            .ForMember(dest=>dest.Group50, opt => opt.MapFrom(src=>src.Group50))
            .ForMember(dest=>dest.Group75, opt => opt.MapFrom(src=>src.Group75))
            .ReverseMap();
            CreateMap<Chat, Domain.Chat.Chat>().ReverseMap();
            CreateMap<Message, Domain.Chat.Message>()
            .ForMember(dest=>dest.Chat, opt => opt.MapFrom(src=>src.Chat))
            .ReverseMap();
        }
    }
}