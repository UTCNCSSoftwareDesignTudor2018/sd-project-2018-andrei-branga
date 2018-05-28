using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.MVC.Models
{
    public class RoomViewModel
    {
        public int RoomId { get; set; }
        public int RoomTypeId { get; set; }
        public int HotelId { get; set; }
        public string RoomName { get; set; }
        public string Details { get; set; }

        public string RoomTypeName { get; set; }
        public string HotelName { get; set; }
    }
}