using Realdeal.Models.User;

namespace Realdeal.Service.User
{
    public interface IUserService
    {
        public string GetCurrentUserId();
        public string GetUserIdByAdvertId(string advertId);
        public UserInformationModel GetUserInfo(string userId);
    }
}