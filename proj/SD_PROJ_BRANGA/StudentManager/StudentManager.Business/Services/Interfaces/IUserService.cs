using Restaurant.Business.Models;

namespace Restaurant.Business.Services.Services
{
    public interface IUserService
    {
        void AddUser(string userId, string username, string firstName, string lastName);
        UserModel GetUserForIdentityId(string id);
        void EditUser(string id, string username, string firstName, string LastName, int? HotelChainId);
    }
}