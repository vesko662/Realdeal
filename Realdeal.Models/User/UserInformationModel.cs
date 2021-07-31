using System;

namespace Realdeal.Models.User
{
    public class UserInformationModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string UserProfilePictureURL { get; set; }
        public DateTime UserSince { get; set; }
    }
}
