using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.MVC.Models
{
    public class BookingViewModel
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int OfferId { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public double PricePerNight { get; set; }
        public double TotalPrice { get; set; }
        public bool Confirmed { get; set; }



        public string Hotel { get; set; }
        public string Chain { get; set; }
        public string RoomType { get; set; }

    }
}