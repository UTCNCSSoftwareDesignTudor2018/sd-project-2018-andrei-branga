using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Restaurant.Business.Models;
using Restaurant.Data.Entities;
using System.Data.Entity;

namespace Restaurant.Business.Services.Services
{
    public class HotelService : IHotelService
    {
        private HotelBookingDBEntities ctx;

        public HotelService(HotelBookingDBEntities ctx)
        {
            this.ctx = ctx;
        }

        public RoomModel GetRoom(int id)
        {
            return AutoMapper.Mapper.Map<RoomModel>(ctx.HotelRooms.Include(p => p.Hotel).Include(p => p.RoomType).Single(p => p.RoomId == id));
        }

        public IQueryable<HotelChainModel> GetAllHotelChains()
        {
            return ctx.HotelChains.ProjectTo<HotelChainModel>();
        }

        public IQueryable<HotelModel> GetAllHotelsInChain(int chainId)
        {
            return ctx.Hotels.Where(p => p.ChainId == chainId).ProjectTo<HotelModel>();
        }

        public void AddHotelToChain(int chainId, string name, string address, string email, string phone)
        {
            Hotel hotel = new Hotel()
            {
                ChainId = chainId,
                HotelName = name,
                HotelAddress = address,
                HotelEmail = email,
                PhoneNumber=phone
            };

            ctx.Hotels.Add(hotel);
            ctx.SaveChanges();
        }

        public void EditHotel(int hotelId, string name, string address, string email, string phone)
        {
            Hotel hotel = ctx.Hotels.Single(p => p.HotelId == hotelId);
            hotel.HotelName = name;
            hotel.HotelAddress = address;
            hotel.HotelEmail = email;
            hotel.PhoneNumber = phone;

            ctx.SaveChanges();
        }

        public void DeleteHotel(int hotelId)
        {
            try
            {
                ctx.Hotels.Remove(ctx.Hotels.Single(p => p.HotelId == hotelId));
                ctx.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Cannot delete hotel because it has related stuff!");
            }
        }



        public IQueryable<RoomTypeModel> GetAllRoomTypes()
        {
            return ctx.RoomTypes.ProjectTo<RoomTypeModel>();
        }

        public void AddRoomToHotel(int hotelId, int roomTypeId, string roomName, string details)
        {
            HotelRoom room=new HotelRoom()
            {
                HotelId = hotelId,
                RoomTypeId = roomTypeId,
                RoomName = roomName,
                Details = details
            };

            ctx.HotelRooms.Add(room);
            ctx.SaveChanges();
        }

        public void DeleteRoomFromHotel(int roomId)
        {
            try
            {
                ctx.HotelRooms.Remove(ctx.HotelRooms.Single(p => p.RoomId == roomId));
                ctx.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Cannot delete room because it has related stuff!");
            }
        }

        public IQueryable<RoomModel> GetAllRoomsFromHotel(int hoteliD)
        {
            return ctx.HotelRooms.Include(p => p.Hotel).Include(p => p.RoomType).Where(p => p.HotelId == hoteliD)
                .ProjectTo<RoomModel>();
        }



        public IQueryable<RoomOfferModel> GetAllRoomOffers(int roomId)
        {
            return ctx.RoomOffers.Include(p=>p.HotelRoom.Hotel.HotelChain)
                .Include(p=>p.HotelRoom.RoomType).Where(p => p.RoomId == roomId).ProjectTo<RoomOfferModel>();
        }

        public void AddRoomOffer(int roomId,DateTime startDate,DateTime endDate,float price)
        {
            RoomOffer offer=new RoomOffer()
            {
                RoomId = roomId,
                StartDate = startDate,
                EndDate = endDate,
                Price = price
            };

            ctx.RoomOffers.Add(offer);
            ctx.SaveChanges();
        }

        public void DeleteRoomOffer(int roomOffer)
        {
            try
            {
                ctx.RoomOffers.Remove(ctx.RoomOffers.Single(p => p.OfferId == roomOffer));
                ctx.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Cannot delete room offer because it has related stuff!");
            }
        }



        public IQueryable<RoomOfferModel> GetSuitableRoomOffers(int roomTypeId, DateTime startDate, DateTime endDate)
        {
            return ctx.RoomOffers.Include(p => p.HotelRoom.Hotel.HotelChain)
                .Include(p => p.HotelRoom.RoomType).Where(
                    p=>p.HotelRoom.RoomType.RoomTypeId==roomTypeId 
                       && p.StartDate<=startDate
                       && p.EndDate>=endDate
                    ).ProjectTo<RoomOfferModel>();
        }



        public IQueryable<BookingModel> GetAllBookingsForUser(int useriD)
        {
            return ctx.Bookings.Include(p=>p.RoomOffer.HotelRoom.Hotel.HotelChain)
                .Include(p=>p.RoomOffer.HotelRoom.RoomType).Where(p => p.UserId == useriD).ProjectTo<BookingModel>();
        }


        public void CancelBooking(int bookingId)
        {
            try
            {
                ctx.Bookings.Remove(ctx.Bookings.Single(p => p.BookingId == bookingId));
                ctx.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Cannot cancel booking because it has related stuff!");
            }
        }


        public void ConfirmBooking(int bookingId)
        {
            var booking = ctx.Bookings.Single(p => p.BookingId == bookingId);
            booking.Confirmed = true;

            ctx.SaveChanges();
        }


        public int AddBooking(int UserId, int OfferId, DateTime start, DateTime end)
        {
            var offer = ctx.RoomOffers.Single(p => p.OfferId == OfferId);

            Booking booking=new Booking()
            {
                UserId = UserId,
                OfferId = OfferId,
                StartDate = start,
                EndDate = end,
                PricePerNight = offer.Price,
                Confirmed = false,

                TotalPrice = offer.Price*((start-end).Days)
            };
            ctx.Bookings.Add(booking);
            ctx.SaveChanges();
            return booking.BookingId;
        }

        
    }
}
