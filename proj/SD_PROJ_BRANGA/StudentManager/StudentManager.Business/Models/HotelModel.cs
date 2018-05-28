using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.Models
{
    public class HotelModel
    {
        public int HotelId { get; set; }
        public int ChainId { get; set; }
        public string HotelName { get; set; }
        public string HotelEmail { get; set; }
        public string HotelAddress { get; set; }
        public string PhoneNumber { get; set; }
    }
}
