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
    public class TheatresControllerTests
    {
        private Mock<ITheatreService> _theatreService;
        private Mock<IAddressService> _addressService;

        [TestMethod]
        public void GetAsync_Return_NewEmptyList()
        {
            // Arrange
            IEnumerable<TheatreDomainModel> theatreDomainModels = null;
            Task<IEnumerable<TheatreDomainModel>> responseTask = Task.FromResult(theatreDomainModels);
            int expectedResultCount = 0;
            int expectedStatusCode = 200;

            _theatreService = new Mock<ITheatreService>();
            _addressService = new Mock<IAddressService>();

            _theatreService.Setup(x => x.GetAllAsync()).Returns(responseTask);
            TheatresController theatresController = new TheatresController(_theatreService.Object, _addressService.Object);

            //Act
            var result = theatresController.GetAsync().ConfigureAwait(false).GetAwaiter().GetResult().Result;
            var resultListObjects = ((OkObjectResult)result).Value;
            var theatreDomainModelResultList = (List<TheatreDomainModel>)resultListObjects;

            //Assert
            Assert.IsNotNull(theatreDomainModelResultList);
            Assert.AreEqual(expectedResultCount, theatreDomainModelResultList.Count);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(expectedStatusCode, ((OkObjectResult)result).StatusCode);
        }

        [TestMethod]
        public void GetAsync_Return_All_Theatres()
        {
            //Arrange
            List<TheatreDomainModel> theatreDomainModelsList = new List<TheatreDomainModel>();
            TheatreDomainModel theatreDomainModel = new TheatreDomainModel
            {
                Id = 123,
                Name = "Teatar1",
                AddressId = 23,
                AuditoriumsList = new List<AuditoriumDomainModel>()
            };

            theatreDomainModelsList.Add(theatreDomainModel);

            IEnumerable<TheatreDomainModel> theatreDomainModels = theatreDomainModelsList;
            Task<IEnumerable<TheatreDomainModel>> responseTask = Task.FromResult(theatreDomainModels);

            int expectedResultCount = 1;
            int expectedStatusCode = 200;

            _theatreService = new Mock<ITheatreService>();
            _addressService = new Mock<IAddressService>();
            _theatreService.Setup(x => x.GetAllAsync()).Returns(responseTask);

            TheatresController theatresController = new TheatresController(_theatreService.Object, _addressService.Object);

            //Act
            var result = theatresController.GetAsync().ConfigureAwait(false).GetAwaiter().GetResult().Result;
            var resultListObjects = ((OkObjectResult)result).Value;
            var theatreDomainModelResultList = (List<TheatreDomainModel>)resultListObjects;

            //Assert
            Assert.IsNotNull(theatreDomainModelResultList);
            Assert.AreEqual(expectedResultCount, theatreDomainModelResultList.Count);
            Assert.AreEqual(theatreDomainModel.Id, theatreDomainModelResultList[0].Id);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(expectedStatusCode, ((OkObjectResult)result).StatusCode);
        }

        [TestMethod]
        public void PostAsync_Create_IsSuccessful_True_Theatre()
        {
            //Arrange
            int expectedStatusCode = 201;

            CreateTheatreModel createTheatreModel = new CreateTheatreModel()
            {
                Name = "bioskop123",
                CityName = "grad",
                SeatRows = 15,
                NumberOfSeats = 11,
                AuditName = "Sala1"
            };

            AddressDomainModel addressDomainModel = new AddressDomainModel
            {
                Id = 123,
                CityName = "grad123",
                StreetName = "ulica123"
            };

            TheatreDomainModel theatreDomainModel = new TheatreDomainModel
            {
                AddressId = 1234,
                Name = createTheatreModel.Name,
                AuditoriumsList = new List<AuditoriumDomainModel>()
            };

            Task<TheatreDomainModel> responseTask = Task.FromResult(theatreDomainModel);
            Task<AddressDomainModel> responseTask2 = Task.FromResult(addressDomainModel);

            _theatreService = new Mock<ITheatreService>();
            _addressService = new Mock<IAddressService>();
            _addressService.Setup(x => x.GetByCityNameAsync(It.IsAny<string>())).Returns(responseTask2);
            _theatreService.Setup(x => x.Create(It.IsAny<TheatreDomainModel>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(responseTask);
            TheatresController theatreController = new TheatresController(_theatreService.Object, _addressService.Object);


            //Act
            var result = theatreController.PostAsync(createTheatreModel).ConfigureAwait(false).GetAwaiter().GetResult().Result;
            var createdResult = ((CreatedResult)result).Value;
            var theatreReturnedModel = (TheatreDomainModel)createdResult;

            //Assert
            Assert.IsNotNull(theatreReturnedModel);
            Assert.IsInstanceOfType(result, typeof(CreatedResult));
            Assert.AreEqual(expectedStatusCode, ((CreatedResult)result).StatusCode);
        }

        [TestMethod]
        public void PostAsync_CreateTheatre_Throw_DbException_Theatre()
        {
            //Arrange 
            string expectedMessage = "Inner exception error message.";
            int expectedStatusCode = 400;

            CreateTheatreModel createTheatreModel = new CreateTheatreModel
            {
                Name = "Bioskop12345",
                CityName = "grad",
                NumberOfSeats = 12,
                SeatRows = 12,
                AuditName = "Sala23"
            };

            TheatreDomainModel theatreDomainModel = new TheatreDomainModel
            {
                Id = 123,
                AddressId = 1423,
                Name = createTheatreModel.Name,
                AuditoriumsList = new List<AuditoriumDomainModel>()
            };

            AddressDomainModel addressDomainModel = new AddressDomainModel
            {
                Id = 123,
                CityName = "grad123",
                StreetName = "ulica123"
            };

            Task<TheatreDomainModel> responseTask = Task.FromResult(theatreDomainModel);
            Task<AddressDomainModel> responseTask2 = Task.FromResult(addressDomainModel);
            Exception exception = new Exception("Inner exception error message.");
            DbUpdateException dbUpdateException = new DbUpdateException("Error.", exception);

            _theatreService = new Mock<ITheatreService>();
            _addressService = new Mock<IAddressService>();

            _theatreService.Setup(x => x.Create(It.IsAny<TheatreDomainModel>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Throws(dbUpdateException);
            _addressService.Setup(x => x.GetByCityNameAsync(It.IsAny<string>())).Returns(responseTask2);
            TheatresController theatresController = new TheatresController(_theatreService.Object, _addressService.Object);

            //Act
            var result = theatresController.PostAsync(createTheatreModel).ConfigureAwait(false).GetAwaiter().GetResult().Result;
            var badObjectResult = ((BadRequestObjectResult)result).Value;
            var errorResult = (ErrorResponseModel)badObjectResult;

            //Assert
            Assert.IsNotNull(errorResult);
            Assert.AreEqual(expectedMessage, errorResult.ErrorMessage);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            Assert.AreEqual(expectedStatusCode, ((BadRequestObjectResult)result).StatusCode);
        }

        [TestMethod]
        public void PostAsync_Create_IsSuccessful_False_Return_BadRequest()
        {
            //Arrange
            string expectedMessage = Messages.THEATRE_CREATION_ERROR;
            int expectedStatusCode = 400;

            CreateTheatreModel createTheatreModel = new CreateTheatreModel
            {
                Name = "bioskop",
                CityName = "grad",
                NumberOfSeats = 13,
                SeatRows = 13,
                AuditName = "Sala12"
            };

            AddressDomainModel addressDomainModel = new AddressDomainModel
            {
                Id = 123,
                CityName = "grad123",
                StreetName = "ulica123"
            };

            TheatreDomainModel theatreDomainModel = null;
            Task<TheatreDomainModel> responseTask = Task.FromResult(theatreDomainModel);
            Task<AddressDomainModel> responseTask2 = Task.FromResult(addressDomainModel);

            _theatreService = new Mock<ITheatreService>();
            _addressService = new Mock<IAddressService>();
            _addressService.Setup(x => x.GetByCityNameAsync(It.IsAny<string>())).Returns(responseTask2);
            _theatreService.Setup(x => x.Create(It.IsAny<TheatreDomainModel>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(responseTask);
            TheatresController theatresController = new TheatresController(_theatreService.Object, _addressService.Object);

            //Act
            var result = theatresController.PostAsync(createTheatreModel).ConfigureAwait(false).GetAwaiter().GetResult().Result;
            var badObjectResult = ((BadRequestObjectResult)result).Value;
            var errorResult = (ErrorResponseModel)badObjectResult;

            //Assert
            Assert.IsNotNull(errorResult);
            Assert.AreEqual(expectedMessage, errorResult.ErrorMessage);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            Assert.AreEqual(expectedStatusCode, ((BadRequestObjectResult)result).StatusCode);
        }

        [TestMethod]
        public void PostAsync_With_Invalid_ModelState_Return_BadRequest()
        {
            //Arrange
            string expectedMessage = "Invalid Model State";
            int expectedStatusCode = 400;

            CreateTheatreModel createTheatreModel = new CreateTheatreModel
            {
                Name = "bioskop",
                CityName = "grad",
                NumberOfSeats = 13,
                SeatRows = 13,
                AuditName = "Sala1"
            };

            _theatreService = new Mock<ITheatreService>();
            _addressService = new Mock<IAddressService>();
            TheatresController theatresController = new TheatresController(_theatreService.Object, _addressService.Object);
            theatresController.ModelState.AddModelError("key", "Invalid Model State");


            //Act
            var result = theatresController.PostAsync(createTheatreModel).ConfigureAwait(false).GetAwaiter().GetResult().Result;
            var resultResponse = (BadRequestObjectResult)result;
            var createdResult = ((BadRequestObjectResult)result).Value;
            var errorResponse = ((SerializableError)createdResult).GetValueOrDefault("key");
            var message = (string[])errorResponse;

            //Assert
            Assert.IsNotNull(resultResponse);
            Assert.AreEqual(expectedMessage, message[0]);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            Assert.AreEqual(expectedStatusCode, resultResponse.StatusCode);
        }

        [TestMethod]
        public void DeleteAsync_DeleteTheatre_IsSuccessful()
        {
            //Arrange
            int theatreDeleteId = 12;
            int expectedStatusCode = 202;

            TheatreDomainModel theatreDomainModel = new TheatreDomainModel
            {
                Id = theatreDeleteId,
                AddressId = 1234,
                Name = "Bioskop",
                AuditoriumsList = new List<AuditoriumDomainModel>()
            };

            Task<TheatreDomainModel> responseTask = Task.FromResult(theatreDomainModel);


            _theatreService = new Mock<ITheatreService>();
            _addressService = new Mock<IAddressService>();
            _theatreService.Setup(x => x.Delete(It.IsAny<int>())).Returns(responseTask);
            TheatresController theatresController = new TheatresController(_theatreService.Object, _addressService.Object);

            //Act
            var result = theatresController.DeleteAsync(theatreDeleteId).ConfigureAwait(false).GetAwaiter().GetResult().Result;
            var objectResult = ((AcceptedResult)result).Value;
            TheatreDomainModel theatreDomainModelResult = (TheatreDomainModel)objectResult;


            //Assert
            Assert.IsNotNull(theatreDomainModel);
            Assert.IsInstanceOfType(result, typeof(AcceptedResult));
            Assert.AreEqual(expectedStatusCode, ((AcceptedResult)result).StatusCode);
            Assert.AreEqual(theatreDeleteId, theatreDomainModelResult.Id);

        }

        [TestMethod]
        public void DeleteAsync_DeleteTheatre_Failed_Throw_DbException()
        {
            //Arrange 
            string expectedMessage = "Inner exception error message.";
            int expectedStatusCode = 400;


            TheatreDomainModel theatreDomainModel = new TheatreDomainModel
            {
                Id = 123,
                AddressId = 555,
                Name = "ime",
                AuditoriumsList = new List<AuditoriumDomainModel>()
            };

            Task<TheatreDomainModel> responseTask = Task.FromResult(theatreDomainModel);
            Exception exception = new Exception("Inner exception error message.");
            DbUpdateException dbUpdateException = new DbUpdateException("Error.", exception);

            _theatreService = new Mock<ITheatreService>();
            _addressService = new Mock<IAddressService>();
            _theatreService.Setup(x => x.Delete(It.IsAny<int>())).Throws(dbUpdateException);
            TheatresController theatresController = new TheatresController(_theatreService.Object, _addressService.Object);

            //Act
            var result = theatresController.DeleteAsync(theatreDomainModel.Id).ConfigureAwait(false).GetAwaiter().GetResult().Result;
            var badObjectResult = ((BadRequestObjectResult)result).Value;
            var errorResult = (ErrorResponseModel)badObjectResult;

            //Assert
            Assert.IsNotNull(errorResult);
            Assert.AreEqual(expectedMessage, errorResult.ErrorMessage);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            Assert.AreEqual(expectedStatusCode, ((BadRequestObjectResult)result).StatusCode);
        }

        [TestMethod]
        public void DeleteAsync_DeleteCinema_Failed_Return_BadRequest()
        {
            //Arrange
            string expectedMessage = Messages.THEATRE_DOES_NOT_EXIST;
            int expectedStatusCode = 400;


            TheatreDomainModel theatreDomainModel = null;

            Task<TheatreDomainModel> responseTask = Task.FromResult(theatreDomainModel);

            _theatreService = new Mock<ITheatreService>();
            _addressService = new Mock<IAddressService>();
            _theatreService.Setup(x => x.Delete(It.IsAny<int>())).Returns(responseTask);
            TheatresController theatresController = new TheatresController(_theatreService.Object, _addressService.Object);

            //Act
            var result = theatresController.DeleteAsync(123).ConfigureAwait(false).GetAwaiter().GetResult().Result;
            var badObjectResult = ((BadRequestObjectResult)result).Value;
            var errorResult = (ErrorResponseModel)badObjectResult;

            //Assert
            Assert.IsNotNull(errorResult);
            Assert.AreEqual(expectedMessage, errorResult.ErrorMessage);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            Assert.AreEqual(expectedStatusCode, ((BadRequestObjectResult)result).StatusCode);
        }
    }
}
