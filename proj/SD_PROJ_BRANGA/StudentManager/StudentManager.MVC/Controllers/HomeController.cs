using DevExpress.Web.Mvc;
using System;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using System.Diagnostics;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using Restaurant.Business;
using Restaurant.Business.Services.Services;
using Restaurant.MVC.Helpers;
using Restaurant.Data;
using Restaurant.Data.Entities;
using Restaurant.MVC.Models;
using Restaurant.MVC.Models.Account;

namespace Restaurant.MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private HotelBookingDBEntities ctx;
        private ServiceFactory serviceFactory;
        private IUserService userService;
        private IHotelService hotelServicé;

        public HomeController()
        {
            this.ctx = new HotelBookingDBEntities();
            this.serviceFactory = new ServiceFactory(ctx);
            this.userService = serviceFactory.GetUserService();
            this.hotelServicé = serviceFactory.GetHotelService();
        }
        public ActionResult Index()
        {

            ViewBag.Name = User.Identity.Name;
            var user = AutoMapper.Mapper.Map<UserViewModel>(
                userService.GetUserForIdentityId(User.Identity.GetUserId()));
            if (User.IsInRole("Manager"))
            {
                ViewBag.HotelChain = hotelServicé.GetAllHotelChains().Single(p => p.HotelChainId == user.HotelChainId)
                    .HotelChainName;
            }

            return View();
        }


        [ValidateInput(false)]
        public ActionResult HotelsManagerGridviewPartial()
        {
            var user = AutoMapper.Mapper.Map<UserViewModel>(
                userService.GetUserForIdentityId(User.Identity.GetUserId()));
            var model = hotelServicé.GetAllHotelsInChain(user.HotelChainId.Value)
                .ProjectTo<HotelViewModel>().ToList();
            return PartialView("HotelManagerGridvIEW", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult HotelsManagerGridviewPartialAddNew(HotelViewModel item)
        {
            var user = AutoMapper.Mapper.Map<UserViewModel>(
                userService.GetUserForIdentityId(User.Identity.GetUserId()));
            if (ModelState.IsValid)
            {
                try
                {
                    hotelServicé.AddHotelToChain(user.HotelChainId.Value,item.HotelName,
                        item.HotelAddress,item.HotelEmail,item.PhoneNumber);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = hotelServicé.GetAllHotelsInChain(user.HotelChainId.Value)
                .ProjectTo<HotelViewModel>().ToList();
            return PartialView("HotelManagerGridvIEW", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult HotelsManagerGridviewPartialUpdate(HotelViewModel item)
        {
            var user = AutoMapper.Mapper.Map<UserViewModel>(
                userService.GetUserForIdentityId(User.Identity.GetUserId()));
            if (ModelState.IsValid)
            {
                try
                {
                    hotelServicé.EditHotel(item.HotelId,item.HotelName,item.HotelAddress,item.HotelEmail,item.PhoneNumber);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = hotelServicé.GetAllHotelsInChain(user.HotelChainId.Value)
                .ProjectTo<HotelViewModel>().ToList();
            return PartialView("HotelManagerGridvIEW", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult HotelsManagerGridviewPartialDelete(HotelViewModel item)
        {
            var user = AutoMapper.Mapper.Map<UserViewModel>(
                userService.GetUserForIdentityId(User.Identity.GetUserId()));
            if (item.HotelId >= 0)
            {
                try
                {
                    hotelServicé.DeleteHotel(item.HotelId);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = hotelServicé.GetAllHotelsInChain(user.HotelChainId.Value)
                .ProjectTo<HotelViewModel>().ToList();
            return PartialView("HotelManagerGridvIEW", model);
        }

        public ActionResult HotelDetails(int hotelId)
        {
            var user = AutoMapper.Mapper.Map<UserViewModel>(
                userService.GetUserForIdentityId(User.Identity.GetUserId()));
            var model = AutoMapper.Mapper.Map<HotelViewModel>(hotelServicé.GetAllHotelsInChain(user.HotelChainId.Value)
                .Single(p => p.HotelId == hotelId));

            return View("HotelDetailsView", model);
        }

        public ActionResult DetailsRoom(int roomId)
        {
            var model = AutoMapper.Mapper.Map<RoomViewModel>(hotelServicé.GetRoom(roomId));

            return View("DetailsRoomView", model);
        }




        //********************************************


        [ValidateInput(false)]
        public ActionResult RoomsGridViewPartial(int hotelId)
        {
            ViewData["HotelId"] = hotelId;

            var model = hotelServicé.GetAllRoomsFromHotel(hotelId).ProjectTo<RoomViewModel>().ToList();
            ViewBag.RoomTypes = hotelServicé.GetAllRoomTypes().ProjectTo<RoomTypeViewModel>().ToList();

            return PartialView("RoomsGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult RoomsGridViewPartialAddNew(RoomViewModel item,int hotelId)
        {
            ViewData["HotelId"] = hotelId;

            if (ModelState.IsValid)
            {
                try
                {
                    hotelServicé.AddRoomToHotel(hotelId,item.RoomTypeId,item.RoomName,item.Details);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            ViewBag.RoomTypes = hotelServicé.GetAllRoomTypes().ProjectTo<RoomTypeViewModel>().ToList();

            var model = hotelServicé.GetAllRoomsFromHotel(hotelId).ProjectTo<RoomViewModel>().ToList();

            return PartialView("RoomsGridViewPartial", model);
        }
        
        [HttpPost, ValidateInput(false)]
        public ActionResult RoomsGridViewPartialDelete(RoomViewModel item,int hotelId)
        {
            ViewData["HotelId"] = hotelId;

            if (item.RoomId >= 0)
            {
                try
                {
                    hotelServicé.DeleteRoomFromHotel(item.RoomId);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = hotelServicé.GetAllRoomsFromHotel(hotelId).ProjectTo<RoomViewModel>().ToList();
            ViewBag.RoomTypes = hotelServicé.GetAllRoomTypes().ProjectTo<RoomTypeViewModel>().ToList();

            return PartialView("RoomsGridViewPartial", model);
        }




        //***************************************


        [ValidateInput(false)]
        public ActionResult RoomOffersGridViewPartial(int roomId)
        {
            ViewData["RoomId"] = roomId;

            var model = hotelServicé.GetAllRoomOffers(roomId).ProjectTo<RoomOfferViewModel>().ToList();

            return PartialView("RoomOfferGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult RoomOffersGridViewPartialAddNew(RoomOfferViewModel item, int roomId)
        {
            ViewData["RoomId"] = roomId;

            if (ModelState.IsValid)
            {
                try
                {
                    hotelServicé.AddRoomOffer(roomId,item.StartDate,item.EndDate,(float)item.Price);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";

            var model = hotelServicé.GetAllRoomOffers(roomId).ProjectTo<RoomOfferViewModel>().ToList();


            return PartialView("RoomOfferGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult RoomOffersGridViewPartialDelete(RoomOfferViewModel item, int roomId)
        {
            ViewData["RoomId"] = roomId;

            if (item.OfferId >= 0)
            {
                try
                {
                    hotelServicé.DeleteRoomOffer(item.OfferId);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = hotelServicé.GetAllRoomOffers(roomId).ProjectTo<RoomOfferViewModel>().ToList();


            return PartialView("RoomOfferGridViewPartial", model);
        }


        public ActionResult SearchView()
        {
            ViewBag.RoomTypes = hotelServicé.GetAllRoomTypes().ProjectTo<RoomTypeViewModel>().ToList();

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchView([ModelBinder(typeof(DevExpress.Web.Mvc.DevExpressEditorsBinder))]  SearchViewModel item)
        {
            return GetSearchResults(item);
        }


        public ActionResult GetSearchResults(SearchViewModel model)
        {
            return PartialView("SearchResultsView",model);
        }

        [ValidateInput(false)]
        public ActionResult RoomOffersResults(DateTime start,DateTime end,int roomType)
        {
            ViewData["Start"] = start;
            ViewData["End"] = end;
            ViewData["Type"] = roomType;


            var model = hotelServicé.GetSuitableRoomOffers(roomType,start,end).ProjectTo<RoomOfferViewModel>().ToList();

            return PartialView("SearchOfferGridView", model);
        }


        [ValidateInput(false)]
        public ActionResult BookingsGridView()
        {
            var user = AutoMapper.Mapper.Map<UserViewModel>(
                userService.GetUserForIdentityId(User.Identity.GetUserId()));
            var model = hotelServicé.GetAllBookingsForUser(user.UserId).ProjectTo<BookingViewModel>().ToList();

            return PartialView(model);
        }

        public ActionResult ConfirmBooking(int offerId,long start,long end)
        {
            DateTime startDate=new DateTime(start);
            DateTime endDate = new DateTime(end);

            var user = AutoMapper.Mapper.Map<UserViewModel>(
                userService.GetUserForIdentityId(User.Identity.GetUserId()));
            int id = hotelServicé.AddBooking(user.UserId, offerId, startDate, endDate);

            var model = AutoMapper.Mapper.Map<BookingViewModel>(hotelServicé.GetAllBookingsForUser(user.UserId)
                .Single(p => p.BookingId == id));

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmBooking([ModelBinder(typeof(DevExpress.Web.Mvc.DevExpressEditorsBinder))]  BookingViewModel item)
        {

            if (ModelState.IsValid)
            {
                hotelServicé.ConfirmBooking(item.BookingId);
                return RedirectToAction("Index");
            }
            else
            {
                return View(item);
            }
        }
    }
}
