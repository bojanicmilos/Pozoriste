using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pozoriste.Data.Entities;
using Pozoriste.Domain.Models;
using Pozoriste.Domain.Services;
using Pozoriste.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pozoriste.Tests.Services
{
    [TestClass]
    public class AddressServiceTests
    {
        private Mock<IAddressesRepository> _mockAddressesRepository;
        private Mock<ITheatreRepository> _mockTheatreRepository;
        private Address _address;
        private Theatre _theatre;
        private AddressDomainModel _addressDomainModel;
        private AddressService _addressService;

        [TestInitialize]
        public void TestInitialize()
        {
            List<Address> address = new List<Address>();
            List<Theatre> theatre = new List<Theatre>();

            _address = new Address
            {
                CityName = "Zrenjanin",
                Id = 1,
                StreetName = "Cara Dusana 1"

            };
            _addressDomainModel = new AddressDomainModel
            {
                CityName = _address.CityName,
                Id = _address.Id,
                StreetName = _address.StreetName

            };

            _mockAddressesRepository = new Mock<IAddressesRepository>();
            _addressService = new AddressService(_mockAddressesRepository.Object);

        }

        [TestMethod]
        public void AddressService_GetAllAddresses_ReturnsListOfAddresses()
        {
            //Arrange 
            List<Theatre> theatres = new List<Theatre>();
            var expectedCount = 1;
            
            List<Address> addresses = new List<Address>();
            addresses.Add(_address);

            List<AddressDomainModel> addressDomainModels = new List<AddressDomainModel>();
            addressDomainModels.Add(_addressDomainModel);

            _mockAddressesRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(addresses);

            //Act
            var resultAction = _addressService.GetAllAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            var result = resultAction.ToList();

            //Assert
            Assert.IsNotNull(resultAction);
            Assert.AreEqual(expectedCount, result.Count);
            Assert.AreEqual(result[0].Id, _addressDomainModel.Id);
            Assert.IsInstanceOfType(result[0], typeof(AddressDomainModel));

        }

        [TestMethod]
        public void AddressService_GetAllAddresses_Return_Null()
        {
            //Arrange
            _mockAddressesRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Address>());

            //Act
            var resultAction = _addressService.GetAllAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            

            //Assert
            Assert.IsNull(resultAction);
        }

       


    }
}
