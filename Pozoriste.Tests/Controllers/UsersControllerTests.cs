using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pozoriste.API.Controllers;
using Pozoriste.Domain.Common;
using Pozoriste.Domain.Interfaces;
using Pozoriste.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pozoriste.Tests.Controllers
{
    [TestClass]
    public class UsersControllerTests
    {
        private Mock<IUserService> _userService;

        [TestMethod]
        public void Get_User_By_UserName_Return_Successful()
        {
            UserDomainModel userDomainModel = new UserDomainModel
            {
                Id = 1,
                UserRole = 0,
                FirstName = "milos",
                LastName = "milosevic",
                UserName = "milosevicmilos"
            };

            Task<UserDomainModel> responseTask = Task.FromResult(userDomainModel);
            int expectedStatusCode = 200;

            _userService = new Mock<IUserService>();
            _userService.Setup(x => x.GetUserByUserName(It.IsAny<string>())).Returns(responseTask);
            UsersController usersController = new UsersController(_userService.Object);

            //Act
            var result = usersController.GetbyUserNameAsync(userDomainModel.UserName).ConfigureAwait(false).GetAwaiter().GetResult().Result;
            var resultObject = ((OkObjectResult)result).Value;
            var userDomainModelResult = (UserDomainModel)resultObject;

            //Assert
            Assert.IsNotNull(userDomainModelResult);
            Assert.AreEqual(userDomainModel.Id, userDomainModelResult.Id);
            Assert.AreEqual(userDomainModel.UserName, userDomainModelResult.UserName);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(expectedStatusCode, ((OkObjectResult)result).StatusCode);
        }

        [TestMethod]
        public void Get_User_By_UserName_Return_Not_Found()
        {
            //Arrange
            UserDomainModel userDomainModel = null;
            string expectedMessage = Messages.USER_NOT_FOUND;

            Task<UserDomainModel> responseTask = Task.FromResult(userDomainModel);

            int expectedStatusCode = 404;

            _userService = new Mock<IUserService>();
            _userService.Setup(x => x.GetUserByUserName(It.IsAny<string>())).Returns(responseTask);
            UsersController usersController = new UsersController(_userService.Object);


            //Act
            var result = usersController.GetbyUserNameAsync("asd").ConfigureAwait(false).GetAwaiter().GetResult().Result;
            var resultObject = ((NotFoundObjectResult)result).Value;
            var messageReturned = (string)resultObject;

            //Assert
            Assert.IsNotNull(messageReturned);
            Assert.AreEqual(expectedMessage, messageReturned);
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.AreEqual(expectedStatusCode, ((NotFoundObjectResult)result).StatusCode);
        }
    }
}
