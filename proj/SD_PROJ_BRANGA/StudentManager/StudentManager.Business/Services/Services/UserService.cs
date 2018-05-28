using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Business.Models;
using Restaurant.Data.Entities;

namespace Restaurant.Business.Services.Services
{
    public class UserService : IUserService
    {
        HotelBookingDBEntities ctx;

        public UserService(HotelBookingDBEntities ctx)
        {
            this.ctx = ctx;
        }

        public void AddUser(string userId, string username, string firstName, string lastName)
        {
            User user = new User()
            {
                Id = userId,
                LastName = lastName,
                FirstName = firstName,
                UserName = username
            };

            ctx.Users.Add(user);
            ctx.SaveChanges();
        }

        public UserModel GetUserForIdentityId(string id)
        {
            return AutoMapper.Mapper.Map<UserModel>(ctx.Users.Single(p=>p.Id==id));
        }

        public void EditUser(string id, string username, string firstName, string LastName, int? HotelChainId)
        {
            var user = ctx.Users.Single(p => p.Id == id);

            user.UserName = username;
            user.FirstName = firstName;
            user.LastName = LastName;
            user.HotelChainId = HotelChainId;
            ctx.SaveChanges();
        }
    }
}
