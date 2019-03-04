using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyUserInfoAPI.Models;
using MyUserInfoAPI.Repos;
using MyUserInfoAPI.Services;


namespace MyUserInfoTests
{
    [TestClass]
    public class UserServiceTest
    {
        [TestMethod]
        public void GetAll_ReturnsAllUsers()
        {
            var mock = new Mock<IRepo<User>>();
            mock.Setup(repo => repo.GetAll()).Returns(GetTestUsers());
            var service = new UserService(mock.Object);

            var result = service.GetAll();

            CollectionAssert.AreEqual(GetTestUsers(), result);
        }

        [TestMethod]
        public async Task GetAllAsync_ReturnsAllUsers()
        {
            var mock = new Mock<IRepo<User>>();
            mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetTestUsers());
            var service = new UserService(mock.Object);

            var result = await service.GetAllAsync();

            var expected = GetTestUsers();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task GetOneAsync_ReturnsCorrectUser()
        {
            var mock = new Mock<IRepo<User>>();
            var user = new User {UserId = 1, FirstName = "Dave", LastName = "Brenner"};
            int userId = 1;
            mock.Setup(repo => repo.GetOneAsync(userId)).ReturnsAsync(user);
            var service = new UserService(mock.Object);

            var result = await service.GetOneAsync(userId);

            Assert.AreEqual(user, result);
        }

        [TestMethod]
        public async Task GetOneAsync_IdOutOfRange_ReturnsNull()
        {
            var mock = new Mock<IRepo<User>>();
            User user = null;
            const int userId = 10;
            mock.Setup(repo => repo.GetOneAsync(userId)).ReturnsAsync(user);
            var service = new UserService(mock.Object);

            var result = await service.GetOneAsync(userId);

            Assert.AreEqual(user, result);
        }

        [TestMethod]
        public async Task GetByFirstNameAsync_ReturnsAllUsersWithDefinedName()
        {
            const string name = "Matt";
            var mock = new Mock<IRepo<User>>();
            mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetTestUsers());
            var service = new UserService(mock.Object);

            var result = await service.GetByFirstNameAsync(name);

            var expected = (from u in GetTestUsers() where u.FirstName.Equals(name, StringComparison.OrdinalIgnoreCase)
                select u).ToList();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task GetByLastNameAsync_ReturnsAllUsersWithDefinedLastName()
        {
            const string name = "Walton";
            var mock = new Mock<IRepo<User>>();

            mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetTestUsers());
            var service = new UserService(mock.Object);

            var result = await service.GetByLastNameAsync(name);

            var expected = (from u in GetTestUsers()
                where u.LastName.Equals(name, StringComparison.OrdinalIgnoreCase)
                select u).ToList();
            CollectionAssert.AreEqual(expected, result);
        }

        private List<User> GetTestUsers()
        {
            var users = new List<User>
            {
                new User {UserId = 1, FirstName = "Dave", LastName = "Brenner"},
                new User {UserId = 2, FirstName = "Matt", LastName = "Walton"},
                new User {UserId = 3, FirstName = "Matt", LastName = "Hagen"},
                new User {UserId = 4, FirstName = "Pat", LastName = "Walton"},
                new User {UserId = 5, FirstName = "Bad", LastName = "User"},
            };
            return users;
        }
    }
}
