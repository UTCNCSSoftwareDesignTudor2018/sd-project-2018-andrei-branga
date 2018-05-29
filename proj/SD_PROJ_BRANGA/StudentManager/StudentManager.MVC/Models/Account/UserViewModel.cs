using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restaurant.MVC.Models.Account
{
    public class UserViewModel
    {
        [Key]
        public int UserId { get; set; }
        public string Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        [EmailAddress]
        public string UserName { get; set; }
        public Nullable<int> HotelChainId { get; set; }

    }
}