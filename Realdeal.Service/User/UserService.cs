using Microsoft.AspNetCore.Http;
using Realdeal.Data;
using Realdeal.Models.User;
using System.Linq;
using System.Security.Claims;
using static Realdeal.Common.GlobalConstants;

namespace Realdeal.Service.User
{
    public class UserService : IUserService
    {
        private readonly RealdealDbContext context;
        private readonly IHttpContextAccessor contextAccessor;

        public UserService(RealdealDbContext context, IHttpContextAccessor contextAccessor)
        {
            this.context = context;
            this.contextAccessor = contextAccessor;
        }

        public string GetCurrentUserId()
        {
            var user = contextAccessor
                .HttpContext.User
                .FindFirst(ClaimTypes.NameIdentifier);

            if(user==null)
            {
                return string.Empty;
            }

            return user.Value;
        }
        
        public string GetUserIdByAdvertId(string advertId)
        {
            var useAdvert = context.Adverts.FirstOrDefault(x => x.Id == advertId);

            if (useAdvert==null)
            {
                return string.Empty;
            }

            return useAdvert.UserId;
        }
        public UserInformationModel GetUserInfo(string userId)
        => context.Users.Where(x => x.Id == userId).Select(s => new UserInformationModel
        {
            Firstname = s.Firstname,
            Lastname = s.Lastname,
            Username = s.UserName,
            UserProfilePictureURL = s.ProfilePhotoUrl,
            UserSince = s.CreteOn,
        }).FirstOrDefault();

        public bool IsUserAdmin()
        => contextAccessor.HttpContext.User.IsInRole(adminRole);
    }
}
