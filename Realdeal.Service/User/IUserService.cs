using Realdeal.Models.User;

namespace Realdeal.Service.User
{
    public interface IUserService
    {
        public string GetCurrentUserId();
        public bool IsUserAdmin();
        public string GetUserIdByUsername(string username);
        public string GetUserIdByAdvertId(string advertId);
        public UserInformationModel GetUserInfo(string userId);
        public string GetUsernameById(string userId);
        public string GetUserFullName(string userId);
    }
}