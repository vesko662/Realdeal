using Xunit;
using System;
using Realdeal.Service.User;
using Moq;
using Realdeal.Data;
using Microsoft.AspNetCore.Http;
using Realdeal.Data.Models;
using Realdeal.Models.User;

namespace Realdeal.Test.Service
{
    public class UserServiceTest
    {
        private IUserService userService;
        private RealdealDbContext context;

        public UserServiceTest()
        {
            context =new TestHelper().CreateDbInMemory();
            var moqHttpContextAccessor = new Mock<IHttpContextAccessor>();
            userService = new UserService(context, moqHttpContextAccessor.Object);
        }

        [Fact]
        public void GetUserIdByUsername_Valid_ShouldReturnUserId()
        {
            //Arange
            var userName = "userTest";
            var expexted = Guid.NewGuid().ToString();

            var user = new ApplicationUser()
            {
                Id = expexted,
                UserName = userName,
            };

            //Act
            context.Users.Add(user);

            context.SaveChanges();

            var userId = this.userService.GetUserIdByUsername(userName);

            // Assert
            Assert.Equal(expexted, userId);
            
        }

        [Fact]
        public void GetUserIdByUsername_Invalid_ShouldNotReturnUserId()
        {
            //Arange
            var userName = "userTest";
            var expexted = Guid.NewGuid().ToString();

            var user = new ApplicationUser()
            {
                Id = expexted,
                UserName = userName,
            };

            //Act
            context.Users.Add(user);

            context.SaveChanges();

            var userId = this.userService.GetUserIdByUsername("notTest");

            // Assert
            Assert.NotEqual(expexted,userId);
        }

        [Fact]
        public void GetUsersCount_ShouldReturnUsersCount()
        {
            //Arange
            var userName = "userTest";
            var userId = Guid.NewGuid().ToString();

            var expexted = 1;

            var user = new ApplicationUser()
            {
                Id = userId,
                UserName = userName,
            };

            //Act
            context.Users.Add(user);

            context.SaveChanges();

            var usersCount = this.userService.GetUsersCount();

            // Assert
            Assert.Equal(expexted, usersCount);
        }
        [Fact]
        public void GetNewUsersCount_ShouldReturnNewUsersCount()
        {
            //Arange
            var userName = "userTest";
            var userId = Guid.NewGuid().ToString();

            var expexted = 1;

            var user = new ApplicationUser()
            {
                Id = userId,
                UserName = userName,
                CreteOn=DateTime.UtcNow,
            };

            //Act
            context.Users.Add(user);

            context.SaveChanges();

            var usersCount = this.userService.GetNewUsersCount();

            // Assert
            Assert.Equal(expexted, usersCount);
        }
        [Fact]
        public void GetUserFullName_Valid_ShouldReturnUserFullname()
        {
            //Arange
            var userId = Guid.NewGuid().ToString();

            var expexted ="user user";

            var user = new ApplicationUser()
            {
                Id = userId,
                CreteOn = DateTime.UtcNow,
                Firstname="user",
                Lastname="user"
            };

            //Act
            context.Users.Add(user);

            context.SaveChanges();

            var userFullName = this.userService.GetUserFullName(userId);

            // Assert
            Assert.Equal(expexted, userFullName);
        }
        [Fact]
        public void GetUserFullName_Invalid_ShouldNotReturnUserFullname()
        {
            //Arange
            var userId = Guid.NewGuid().ToString();

            var expexted = "user user";

            var user = new ApplicationUser()
            {
                Id = userId,
                CreteOn = DateTime.UtcNow,
                Firstname = "user",
                Lastname = "user"
            };

            //Act
            context.Users.Add(user);

            context.SaveChanges();

            var userFullName = this.userService.GetUserFullName("iD");

            // Assert
            Assert.NotEqual(expexted, userFullName);
        }
        [Fact]
        public void GetUsernameById_Valid_ShouldReturnUserUsername()
        {
            //Arange
            var userId = Guid.NewGuid().ToString();
            var expexted = "user";

            var user = new ApplicationUser()
            {
                Id = userId,
                UserName=expexted,
            };

            //Act
            context.Users.Add(user);

            context.SaveChanges();

            var userFullName = this.userService.GetUsernameById(userId);

            // Assert
            Assert.Equal(expexted, userFullName);
        }
        [Fact]
        public void GetUsernameById_Notvalid_ShouldNotReturnUserUsername()
        {
            //Arange
            var userId = Guid.NewGuid().ToString();
            var expexted = "user";

            var user = new ApplicationUser()
            {
                Id = userId,
                UserName = expexted,
            };

            //Act
            context.Users.Add(user);

            context.SaveChanges();

            var userFullName = this.userService.GetUsernameById("Id");

            // Assert
            Assert.NotEqual(expexted, userFullName);
        }
        [Fact]
        public void GetUserInfo_ShouldReturnUserInformationModelWithUserData()
        {
            //Arange
            var userId = Guid.NewGuid().ToString();
            var userName = "user";

            var user = new ApplicationUser()
            {
                Id = userId,
                UserName = userName,
                ProfilePhotoUrl=null,
            };

            var expected = new UserInformationModel() { Username = userName };

            //Act
            context.Users.Add(user);

            context.SaveChanges();

            var userInf = this.userService.GetUserInfo(userId);

            // Assert
            Assert.Equal(expected.Username, userInf.Username);
        }
    }
}
