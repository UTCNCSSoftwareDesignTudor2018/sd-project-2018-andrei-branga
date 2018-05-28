using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.MVC.Models
{
    public class SearchViewModel
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int RoomType { get; set; }

    }
}