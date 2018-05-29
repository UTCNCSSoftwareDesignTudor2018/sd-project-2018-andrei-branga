//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Restaurant.Data.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Booking
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int OfferId { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public double PricePerNight { get; set; }
        public double TotalPrice { get; set; }
        public bool Confirmed { get; set; }
    
        public virtual RoomOffer RoomOffer { get; set; }
        public virtual User User { get; set; }
    }
}