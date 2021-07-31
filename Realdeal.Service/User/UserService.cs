using Microsoft.AspNetCore.Http;
using Realdeal.Data;
using Realdeal.Models.User;
using System.Linq;
using System.Security.Claims;

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
         => this.contextAccessor
                .HttpContext.User
                .FindFirst(ClaimTypes.NameIdentifier)
                .Value;

        public string GetUserIdByAdvertId(string advertId)
            => context.Adverts.FirstOrDefault(x => x.Id == advertId).UserId;
        public UserInformationModel GetUserInfo(string userId)
        => context.Users.Where(x => x.Id == userId).Select(s => new UserInformationModel
        {
            Firstname = s.Firstname,
            Lastname = s.Lastname,
            Username = s.UserName,
            UserProfilePictureURL = s.ProfilePhotoUrl,
            UserSince = s.CreteOn,
        }).FirstOrDefault();
    }
}
