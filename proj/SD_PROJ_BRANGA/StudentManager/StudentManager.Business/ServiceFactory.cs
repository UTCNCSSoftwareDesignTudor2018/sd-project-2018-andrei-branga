using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Business.Services.Services;
using Restaurant.Data.Entities;

namespace Restaurant.Business
{
    public  class ServiceFactory
    {
        private HotelBookingDBEntities ctx;
        public ServiceFactory(HotelBookingDBEntities ctx)
        {
            this.ctx = ctx;
        }

        public UserService GetUserService()
        {
            return new UserService(ctx);
        }

        public HotelService GetHotelService()
        {
            return new HotelService(ctx);
        }

    }
}
