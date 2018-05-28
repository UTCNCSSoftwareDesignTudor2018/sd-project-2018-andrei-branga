using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.Models
{
    public class RoomOfferModel
    {
        public int OfferId { get; set; }
        public int RoomId { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public double Price { get; set; }



        public string RoomName { get; set; }
        public string RoomType { get; set; }

        public string Hotel { get; set; }
        public string Chain { get; set; }



    }
}
