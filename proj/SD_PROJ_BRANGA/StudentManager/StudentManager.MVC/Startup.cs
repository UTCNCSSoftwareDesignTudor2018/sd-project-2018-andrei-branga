using AutoMapper;
using Microsoft.Owin;
using Owin;
using Restaurant.Business.Models;
using Restaurant.Data.Entities;
using Restaurant.MVC.Models;
using Restaurant.MVC.Models.Account;

[assembly: OwinStartupAttribute(typeof(Restaurant.MVC.Startup))]
namespace Restaurant.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserModel>();
                cfg.CreateMap<UserModel, UserViewModel>();

                cfg.CreateMap<HotelChain, HotelChainModel>();
                cfg.CreateMap<HotelChainModel, HotelChainViewModel>();

                cfg.CreateMap<Hotel, HotelModel>();
                cfg.CreateMap<HotelModel, HotelViewModel>();

                cfg.CreateMap<RoomType, RoomTypeModel>();
                cfg.CreateMap<RoomTypeModel, RoomTypeViewModel>();

                cfg.CreateMap<HotelRoom, RoomModel>()
                    .ForMember(dst=>dst.HotelName,opt=>opt.MapFrom(src=>src.Hotel.HotelName))
                    .ForMember(dst=>dst.RoomTypeName,opt=>opt.MapFrom(src=>src.RoomType.RoomType1));
                    ;
                cfg.CreateMap<RoomModel, RoomViewModel>();


                cfg.CreateMap<RoomOffer, RoomOfferModel>()
                    .ForMember(dst => dst.RoomName, opt => opt.MapFrom(src => src.HotelRoom.RoomName))
                    .ForMember(dst => dst.RoomType, opt => opt.MapFrom(src => src.HotelRoom.RoomType.RoomType1))
                    .ForMember(dst => dst.Hotel, opt => opt.MapFrom(src => src.HotelRoom.Hotel.HotelName))
                    .ForMember(dst => dst.Chain, opt => opt.MapFrom(src => src.HotelRoom.Hotel.HotelChain.HotelChainName));
                
                cfg.CreateMap<RoomOfferModel, RoomOfferViewModel>();

            });
        }
    }
}
