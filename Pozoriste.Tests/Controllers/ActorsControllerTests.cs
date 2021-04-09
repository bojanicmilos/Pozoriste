using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pozoriste.API.Controllers;
using Pozoriste.API.Models;
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
    public class ActorsControllerTests
    {
        private Mock<IActorService> _actorService;

        [TestMethod]
        public void GetAsync_Return_All_Actors()
        {
            //Arrange
            ActorDomainModel actorDomainModel = new ActorDomainModel
            {
                Id = 1,
                FirstName = "milan",
                LastName = "petrovic"
            };
            int expectedStatusCode = 200;
            int expectedResultCount = 1;

            List<ActorDomainModel> actorDomainModels = new List<ActorDomainModel>();
            actorDomainModels.Add(actorDomainModel);

            IEnumerable<ActorDomainModel> actorsIEn = actorDomainModels;
            Task<IEnumerable<ActorDomainModel>> responseTask = Task.FromResult(actorsIEn);

            _actorService = new Mock<IActorService>();
            _actorService.Setup(x => x.GetAllAsync()).Returns(responseTask);
            ActorsController actorsController = new ActorsController(_actorService.Object);

            //Act
            var result = actorsController.GetAllActors().ConfigureAwait(false).GetAwaiter().GetResult().Result;
            var resultList = ((OkObjectResult)result).Value;
            List<ActorDomainModel> actorDomainModelsResult = (List<ActorDomainModel>)resultList;

            //Assert
            Assert.IsNotNull(actorDomainModelsResult);
            Assert.AreEqual(expectedResultCount, actorDomainModelsResult.Count);
            Assert.AreEqual(actorDomainModel.Id, actorDomainModelsResult[0].Id);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(expectedStatusCode, ((OkObjectResult)result).StatusCode);
        }

        [TestMethod]
        public void Get_Async_Return_Empty_List()
        {
            //Arrange
            int expectedStatusCode = 200;

            IEnumerable<ActorDomainModel> actorsIEn = null;
            Task<IEnumerable<ActorDomainModel>> responseTask = Task.FromResult(actorsIEn);

            _actorService = new Mock<IActorService>();
            _actorService.Setup(x => x.GetAllAsync()).Returns(responseTask);
            ActorsController actorsController = new ActorsController(_actorService.Object);

            //Act
            var result = actorsController.GetAllActors().ConfigureAwait(false).GetAwaiter().GetResult().Result;
            var resultList = ((OkObjectResult)result).Value;
            List<ActorDomainModel> actorDomainModelsResult = (List<ActorDomainModel>)resultList;

            //Assert
            Assert.IsNotNull(actorDomainModelsResult);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(expectedStatusCode, ((OkObjectResult)result).StatusCode);
        }

        [TestMethod]
        public void GetByIdAsync_Return_Actor_OkObjectResult()
        {
            //Arrange
            ActorDomainModel actorDomainModel = new ActorDomainModel
            {
                Id = 1,
                FirstName = "milan",
                LastName = "petrovic"
            };
            int expectedStatusCode = 200;

            Task<ActorDomainModel> responseTask = Task.FromResult(actorDomainModel);

            _actorService = new Mock<IActorService>();
            _actorService.Setup(x => x.GetByIdAsync(It.IsAny<int>())).Returns(responseTask);
            ActorsController actorsController = new ActorsController(_actorService.Object);

            //Act
            var result = actorsController.GetActorById(actorDomainModel.Id).ConfigureAwait(false).GetAwaiter().GetResult().Result;
            var resultList = ((OkObjectResult)result).Value;
            ActorDomainModel actorDomainModelsResult = (ActorDomainModel)resultList;

            //Assert
            Assert.IsNotNull(actorDomainModelsResult);
            Assert.AreEqual(actorDomainModel.FirstName, actorDomainModelsResult.FirstName);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(expectedStatusCode, ((OkObjectResult)result).StatusCode);
        }

        [TestMethod]
        public void GetByIdAsync_Return_Not_Found()
        {
            //Arrange
            ActorDomainModel actorDomainModel = null;
            int expectedStatusCode = 404;
            string expectedMessage = Messages.ACTOR_DOES_NOT_EXIST;

            Task<ActorDomainModel> responseTask = Task.FromResult(actorDomainModel);

            _actorService = new Mock<IActorService>();
            _actorService.Setup(x => x.GetByIdAsync(It.IsAny<int>())).Returns(responseTask);
            ActorsController actorsController = new ActorsController(_actorService.Object);

            //Act
            var result = actorsController.GetActorById(1).ConfigureAwait(false).GetAwaiter().GetResult().Result;
            var resultList = ((NotFoundObjectResult)result).Value;
            string actorDomainModelsResult = (string)resultList;

            //Assert
            Assert.IsNotNull(actorDomainModelsResult);
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.AreEqual(expectedStatusCode, ((NotFoundObjectResult)result).StatusCode);
            Assert.AreEqual(expectedMessage, actorDomainModelsResult);
        }

        [TestMethod]
        public void CreateActorAsync_Create_IsSuccessful_True_Actor()
        {
            // Arrange
            int expectedStatusCode = 201;

            ActorModel actorModel = new ActorModel()
            {
                FirstName = "Ime",
                LastName = "Prezime"
            };

            ActorDomainModel actorDomainModel = new ActorDomainModel
            {
                Id = 1,
                FirstName = actorModel.FirstName,
                LastName = actorModel.LastName
            };

            Task<ActorDomainModel> responseTask = Task.FromResult(actorDomainModel);

            _actorService = new Mock<IActorService>();
            _actorService.Setup(x => x.AddActor(It.IsAny<ActorDomainModel>())).Returns(responseTask);
            ActorsController actorsController = new ActorsController(_actorService.Object);

            // Act
            var result = actorsController.AddActor(actorModel).ConfigureAwait(false).GetAwaiter().GetResult().Result;
            var createdResult = ((CreatedAtActionResult)result).Value;
            var actorDomainModel1 = (ActorDomainModel)createdResult;

            // Assert
            Assert.IsNotNull(actorDomainModel1);
            Assert.AreEqual(actorModel.FirstName, actorDomainModel1.FirstName);
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
            Assert.AreEqual(expectedStatusCode, ((CreatedAtActionResult)result).StatusCode);
        }

        [TestMethod]
        public void CreateActorAsync_Create_Throw_DbException_Actor()
        {
            // Arrange
            string expectedMessage = "Inner exception error message.";
            int expectedStatusCode = 400;

            ActorModel actorModel = new ActorModel()
            {
                FirstName = "Ime",
                LastName = "Prezime"
            };

            ActorDomainModel actorDomainModel = new ActorDomainModel
            {
                Id = 1,
                FirstName = actorModel.FirstName,
                LastName = actorModel.LastName
            };

            Task<ActorDomainModel> responseTask = Task.FromResult(actorDomainModel);
            Exception exception = new Exception("Inner exception error message.");
            DbUpdateException dbUpdateException = new DbUpdateException("Error.", exception);

            _actorService = new Mock<IActorService>();
            _actorService.Setup(x => x.AddActor(It.IsAny<ActorDomainModel>())).Throws(dbUpdateException);
            ActorsController actorsController = new ActorsController(_actorService.Object);

            // Act
            var result = actorsController.AddActor(actorModel).ConfigureAwait(false).GetAwaiter().GetResult().Result;
            var resultResponse = (BadRequestObjectResult)result;
            var badObjectResult = ((BadRequestObjectResult)result).Value;
            var errorResult = (ErrorResponseModel)badObjectResult;

            // Assert
            Assert.IsNotNull(resultResponse);
            Assert.AreEqual(expectedMessage, errorResult.ErrorMessage);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            Assert.AreEqual(expectedStatusCode, resultResponse.StatusCode);
        }
    }
}
