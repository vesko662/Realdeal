using Moq;
using Realdeal.Data;
using Realdeal.Data.Models;
using Realdeal.Models.Message;
using Realdeal.Service.Message;
using Realdeal.Service.User;
using System;
using System.Linq;
using Xunit;

namespace Realdeal.Test.Service
{
    public class MessageServiceTest
    {
        private RealdealDbContext context;

        public MessageServiceTest()
        {
            context = new TestHelper().CreateDbInMemory();
        }

        [Fact]
        public void CreateMessage_Valid_ShouldCreateMessage()
        {
            var moqUserService = new Mock<IUserService>();
            var messageService = new MessageService(moqUserService.Object, context);

            var createAdvert = new Advert()
            {
                Id = "advertId",
            };

            context.Adverts.Add(createAdvert);

            context.SaveChanges();

            var message = messageService.CreateMessage("sender", "reciever","advertId", "content");

            var result = context.Messages.Contains(message);

            Assert.True(result);
        }
        [Fact]
        public void CreateMessage_InvalidAdvertId_ShouldNotCreateMessage()
        {
            var moqUserService = new Mock<IUserService>();
            var messageService = new MessageService(moqUserService.Object, context);

            var createAdvert = new Advert()
            {
                Id = "advertId",
            };

            context.Adverts.Add(createAdvert);

            context.SaveChanges();

            var message = messageService.CreateMessage("sender", "reciever", "wrongId", "content");

            var result = context.Messages.Contains(message);

            Assert.False(result);
        }
        [Fact]
        public void GetMessageViewModel_ShouldReturnGetMessageViewModel()
        {
            var moqUserService = new Mock<IUserService>();
            moqUserService.Setup(x => x.GetUserFullName("name"))
               .Returns("fullName");
            var messageService = new MessageService(moqUserService.Object, context);

            var expectedMessageModelValue = new MessageViewModel()
            {
                SenderId = "senderId",
                RecieverId = "recieverId",
                AdvertId = "advertId",
            };

            var messageModel = messageService.GetMessageViewModel("advertId", "senderId", "recieverId");

            Assert.IsType<MessageViewModel>(messageModel);
            Assert.Equal(expectedMessageModelValue.SenderId, messageModel.SenderId);
            Assert.Equal(expectedMessageModelValue.AdvertId, messageModel.AdvertId);
            Assert.Equal(expectedMessageModelValue.RecieverId, messageModel.RecieverId);
        }
        [Fact] 
        public void GetMessagesForAdvert_ShouldGetAdvertMessages()
        {
            var user1Id = Guid.NewGuid().ToString();
            var user2Id = Guid.NewGuid().ToString();
            var advertId = Guid.NewGuid().ToString();

            var moqUserService = new Mock<IUserService>();
            moqUserService.Setup(x => x.GetUserFullName("name"))
               .Returns("fullName");
            var messageService = new MessageService(moqUserService.Object, context);

            var user1 = new ApplicationUser()
            {
                Id = user1Id
            };
            var user2 = new ApplicationUser()
            {
                Id = user2Id
            };

            context.Users.AddRange(user1, user2);

            var advert = new Advert()
            {
                Id = advertId,
            };

            context.Adverts.Add(advert);

            var message = new Message()
            {
                SenderId = user1Id,
                ReciverId = user2Id,
                AdvertId = advertId,
                Content = "content"
            };
            context.Messages.Add(message);

            context.SaveChanges();

            var advertMessages = messageService.GetMessagesForAdvert(advertId,user1Id,user2Id);

            Assert.True(advertMessages.Any());
            Assert.Equal(advertMessages.First().AdvertId,advertId);
            Assert.Equal(advertMessages.First().SenderId, user1Id);
            Assert.Equal(advertMessages.First().RecieverId, user2Id);

        }
        [Fact]
        public void DeleteAllMessagesToAdvert_ShouldDeleteAllAdvertMessagesWhenAdvertIsDeleted()
        {
            var advertId = Guid.NewGuid().ToString();
            var expexted = 0;

            var moqUserService = new Mock<IUserService>();
            var messageService = new MessageService(moqUserService.Object, context);
            var advert = new Advert()
            {
                Id = advertId,
            };

            context.Adverts.Add(advert);

            var message = new Message()
            {
                AdvertId = advertId,
                Content = "content"
            };
            context.Messages.Add(message);

            context.SaveChanges();

            messageService.DeleteAllMessagesToAdvert(advertId);

            var result = context.Messages.Where(x => x.AdvertId == advertId).Count();

            Assert.Equal(expexted, result);
        }
        [Fact]
        public void DeleteAllMessagesToAdvert_InvalidAdverId_ShouldNotDeleteAllAdvertMessagesWhenAdvertIsDeleted()
        {
            var advertId = Guid.NewGuid().ToString();
            var expexted = 1;

            var moqUserService = new Mock<IUserService>();
            var messageService = new MessageService(moqUserService.Object, context);
            var advert = new Advert()
            {
                Id = advertId,
            };

            context.Adverts.Add(advert);

            var message = new Message()
            {
                AdvertId = advertId,
                Content = "content"
            };
            context.Messages.Add(message);

            context.SaveChanges();

            messageService.DeleteAllMessagesToAdvert("");

            var result = context.Messages.Where(x => x.AdvertId == advertId).Count();

            Assert.Equal(expexted, result);
        }

    }
}
