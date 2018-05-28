using System;
using System.Linq;
using Restaurant.Business.Models;

namespace Restaurant.Business.Services.Services
{
    public interface IHotelService
    {
        IQueryable<HotelChainModel> GetAllHotelChains();
        IQueryable<HotelModel> GetAllHotelsInChain(int chainId);
        void AddHotelToChain(int chainId, string name, string address, string email, string phone);
        void EditHotel(int hotelId, string name, string address, string email, string phone);
        void DeleteHotel(int hotelId);
        IQueryable<RoomTypeModel> GetAllRoomTypes();
        void AddRoomToHotel(int hotelId, int roomTypeId, string roomName, string details);
        void DeleteRoomFromHotel(int roomId);
        IQueryable<RoomModel> GetAllRoomsFromHotel(int hoteliD);

        IQueryable<RoomOfferModel> GetAllRoomOffers(int roomId);
        void AddRoomOffer(int roomId, DateTime startDate, DateTime endDate, float price);
        void DeleteRoomOffer(int roomOffer);
        RoomModel GetRoom(int id);
        IQueryable<RoomOfferModel> GetSuitableRoomOffers(int roomTypeId, DateTime startDate, DateTime endDate);
    }
}