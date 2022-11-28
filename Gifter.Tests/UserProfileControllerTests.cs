using Gifter.Controllers;
using Gifter.Models;
using Gifter.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xunit;

namespace Gifter.Tests
{
    public class UserProfileControllerTests
    {
        [Fact]
        public void Get_Returns_All_UserProfiles()
        {
            // Arrange 
            var userCount = 20;
            var users = CreateTestUsers(userCount);

            var repo = new InMemoryUserProfileRepository(users);
            var controller = new UserProfileController(repo);

            // Act 
            var result = controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualUsers = Assert.IsType<List<UserProfile>>(okResult.Value);

            Assert.Equal(userCount, actualUsers.Count);
            Assert.Equal(users, actualUsers);
        }

        [Fact]
        public void Get_By_Id_Returns_NotFound_When_Given_Unknown_Id()
        {
            // Arrange
            var users = new List<UserProfile>();

            var repo = new InMemoryUserProfileRepository(users);
            var controller = new UserProfileController(repo);

            // Act
            var result = controller.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Get_By_Id_Returns_UserProfile_With_Given_Id()
        {
            // Arrange
            var testUserId = 99;
            var users = CreateTestUsers(5);
            users[0].Id = testUserId;

            var repo = new InMemoryUserProfileRepository(users);
            var controller = new UserProfileController(repo);

            // Act
            var result = controller.Get(testUserId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualPost = Assert.IsType<UserProfile>(okResult.Value);

            Assert.Equal(testUserId, actualPost.Id);
        }

        [Fact]
        public void Post_Method_Adds_A_New_UserProfile()
        {
            var userCount = 20;
            var users = CreateTestUsers(userCount);

            var repo = new InMemoryUserProfileRepository(users);
            var controller = new UserProfileController(repo);

            var newUser = new UserProfile()
            {
                Name = "Test",
                Email = "Test",
                ImageUrl = "Test",
                Bio = "Test",
                DateCreated = DateTime.Today
            };

            controller.Post(newUser);

            Assert.Equal(userCount + 1, repo.InternalData.Count);    
        }

        private List<UserProfile> CreateTestUsers(int count)
        {
            var users = new List<UserProfile>();
            for (var i = 1; i <= count; i++)
            {
                users.Add(new UserProfile()
                {
                    Id = i,
                    Name = $"Name {i}",
                    Email = $"Email {i}",
                    Bio = $"Bio {i}",
                    ImageUrl = $"http://post.image.url/{i}",
                    DateCreated = DateTime.Today.AddDays(-i),
                });
            }
            return users;
        }
    }
}
