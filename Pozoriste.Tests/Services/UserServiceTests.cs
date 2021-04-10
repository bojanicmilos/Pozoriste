using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pozoriste.Data.Entities;
using Pozoriste.Data.Enums;
using Pozoriste.Domain.Models;
using Pozoriste.Domain.Services;
using Pozoriste.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pozoriste.Tests.Services
{
    [TestClass]
    public class UserServiceTests
    {
        private Mock<IUsersRepository> _mockUserRepository;
        private UserService _userService;
        private User _user;
        private UserDomainModel _userDomainModel;

        [TestInitialize]
        public void TestInitialize()
        {
            _user = new User
            {
                Id = 1,
                FirstName = "Milos",
                LastName = "Bojanic",
                UserRole = UserRole.ADMIN,
                UserName = "milosbojanic" 
            };

            _userDomainModel = new UserDomainModel
            {
                Id = _user.Id,
                FirstName = _user.FirstName,
                LastName = _user.LastName,
                UserRole = UserRole.ADMIN,
                UserName = _user.UserName,
            };

            _mockUserRepository = new Mock<IUsersRepository>();
            _userService = new UserService(_mockUserRepository.Object);
        }

        [TestMethod]
        public void Get_User_By_UserName_Async_Return_User()
        {
            // Arrange 
            _mockUserRepository.Setup(x => x.GetByUserName(It.IsAny<string>())).ReturnsAsync(_user);

            // Act
            var resultObject = _userService.GetUserByUserName(_user.UserName).ConfigureAwait(false).GetAwaiter().GetResult();

            // Assert
            Assert.IsNotNull(resultObject);
            Assert.AreEqual(resultObject.Id, _user.Id);
            Assert.IsTrue((int)resultObject.UserRole == 1);
        }

        [TestMethod]
        public void Get_User_By_UserName_Async_Return_Null()
        {
            // Arrange
            _mockUserRepository.Setup(x => x.GetByUserName(It.IsAny<string>())).ReturnsAsync(null as User);

            // Act
            var resultObject = _userService
                .GetUserByUserName(_user.UserName)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.IsNull(resultObject);
        }
    }
}
