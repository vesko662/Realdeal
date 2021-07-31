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

        public string GetUserId()
         => this.contextAccessor
                .HttpContext.User
                .FindFirst(ClaimTypes.NameIdentifier)
                .Value;

        public UserInformationModel GetUserInfo()
        => context.Users.Where(x=>x.Id== GetUserId()).Select(s=>new UserInformationModel
        {
            Firstname=s.Firstname,
            Lastname = s.Lastname,
            Username = s.UserName,
            UserProfilePictureURL = s.ProfilePhotoUrl,
            UserSince=s.

        })
    }
}
