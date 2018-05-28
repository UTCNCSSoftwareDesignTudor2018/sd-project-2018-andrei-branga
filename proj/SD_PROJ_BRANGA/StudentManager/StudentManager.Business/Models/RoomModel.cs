using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.Models
{
    public class RoomModel
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
