using Microsoft.AspNetCore.Http;
using Realdeal.Data;
using Realdeal.Models.User;
using System;
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

            if (user == null)
            {
                return string.Empty;
            }

            return user.Value;
        }

        public int GetNewUsersCount()
        => context.Users
            .Where(x => x.CreteOn.Date == DateTime.UtcNow.Date && x.CreteOn.Year == DateTime.UtcNow.Year)
            .Count();

        public string GetUserFullName(string userId)
        {

            var user = context.Users.FirstOrDefault(x => x.Id == userId);

            if (user == null)
            {
                return string.Empty;
            }

            return user.Firstname + " " + user.Lastname;
        }

        public string GetUserIdByAdvertId(string advertId)
        {
            var useAdvert = context.Adverts.FirstOrDefault(x => x.Id == advertId);

            if (useAdvert == null)
            {
                return string.Empty;
            }

            return useAdvert.UserId;
        }

        public string GetUserIdByUsername(string username)
        {
            var user = context.Users.FirstOrDefault(x => x.UserName == username);

            if (user == null)
            {
                return string.Empty;
            }

            return user.Id;
        }

        public UserInformationModel GetUserInfo(string userId)
        => context.Users
            .Where(x => x.Id == userId)
            .Select(s => new UserInformationModel
            {
                Firstname = s.Firstname,
                Lastname = s.Lastname,
                Username = s.UserName,
                UserProfilePictureURL = s.ProfilePhotoUrl,
                UserSince = s.CreteOn,
            })
            .FirstOrDefault();

        public string GetUsernameById(string userId)
        {
            var user = context.Users.FirstOrDefault(x => x.Id == userId);

            if (user == null)
            {
                return string.Empty;
            }

            return user.UserName;
        }

        public string GetUserProfilePhoto()
       => context.Users.FirstOrDefault(x => x.Id == GetCurrentUserId()).ProfilePhotoUrl;

        public int GetUsersCount()
       => context.Users.Count();

        public bool IsUserAdmin()
        => contextAccessor.HttpContext.User.IsInRole(adminRole);
    }
}
