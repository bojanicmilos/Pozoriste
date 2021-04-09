using Microsoft.AspNetCore.Mvc;
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
    public class AddressesControllerTests
    {
        private Mock<IAddressService> _addressService;

        [TestMethod]
        public void GetAsync_Return_All_Addresses()
        {
            //Arrange
            List<AddressDomainModel> addressesDomainModelsList = new List<AddressDomainModel>();
            AddressDomainModel addressDomainModel = new AddressDomainModel
            {
                Id = 1,
                CityName = "ImeGrada",
                StreetName = "ImeUlice"
            };
            addressesDomainModelsList.Add(addressDomainModel);
            IEnumerable<AddressDomainModel> addressDomainModels = addressesDomainModelsList;
            Task<IEnumerable<AddressDomainModel>> responseTask = Task.FromResult(addressDomainModels);
            int expectedResultCount = 1;
            int expectedStatusCode = 200;

            _addressService = new Mock<IAddressService>();
            _addressService.Setup(x => x.GetAllAsync()).Returns(responseTask);
            AddressesController addressesController = new AddressesController(_addressService.Object);

            //Act
            var result = addressesController.GetAllAddresses().ConfigureAwait(false).GetAwaiter().GetResult().Result;
            var resultList = ((OkObjectResult)result).Value;
            var addressDomainModelResultList = (List<AddressDomainModel>)resultList;

            //Assert
            Assert.IsNotNull(addressDomainModelResultList);
            Assert.AreEqual(expectedResultCount, addressDomainModelResultList.Count);
            Assert.AreEqual(addressDomainModel.Id, addressDomainModelResultList[0].Id);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(expectedStatusCode, ((OkObjectResult)result).StatusCode);
        }

        [TestMethod]
        public void GetAddress_ById_Async_Return_Address_OkObjectResult()
        {
            //Arrange
            AddressDomainModel addressDomainModel = new AddressDomainModel
            {
                Id = 123,
                CityName = "Zrenjanin",
                StreetName = "Nikole Pasica"
            };

            int expectedStatusCode = 200;
            Task<AddressDomainModel> responseTask = Task.FromResult(addressDomainModel);
            _addressService = new Mock<IAddressService>();
            _addressService.Setup(x => x.GetByIdAsync(It.IsAny<int>())).Returns(responseTask);
            AddressesController addressController = new AddressesController(_addressService.Object);

            //Act
            var result = addressController.GetAddressById(addressDomainModel.Id).ConfigureAwait(false).GetAwaiter().GetResult().Result;
            var objectResult = ((OkObjectResult)result).Value;
            AddressDomainModel addressDomainResult = (AddressDomainModel)objectResult;

            //Assert
            Assert.IsNotNull(objectResult);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(expectedStatusCode, ((OkObjectResult)result).StatusCode);
            Assert.AreEqual(addressDomainModel.Id, addressDomainResult.Id);
        }

        [TestMethod]
        public void GetAddress_ById_Async_Return_Not_Found()
        {
            //Arrange
            string expectedMessage = Messages.ADDRESS_NOT_FOUND;
            int expectedStatusCode = 404;
            AddressDomainModel addressDomainModel = null;
            Task<AddressDomainModel> responseTask = Task.FromResult(addressDomainModel);
            _addressService = new Mock<IAddressService>();
            _addressService.Setup(x => x.GetByIdAsync(It.IsAny<int>())).Returns(responseTask);
            AddressesController addressController = new AddressesController(_addressService.Object);

            //Act
            var result = addressController.GetAddressById(123).ConfigureAwait(false).GetAwaiter().GetResult().Result;
            var objectResult = ((NotFoundObjectResult)result).Value;
            string message = (string)objectResult;

            //Assert
            Assert.IsNotNull(objectResult);
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.AreEqual(expectedStatusCode, ((NotFoundObjectResult)result).StatusCode);
            Assert.AreEqual(expectedMessage, message);
        }
    }
}
