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

namespace Pozoriste.Tests.Services
{
    [TestClass]
    public class SeatServiceTests
    {
        private Mock<ISeatsRepository> _mockSeatRepository;
        private SeatService _seatService;
        private Seat _seat;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockSeatRepository = new Mock<ISeatsRepository>();
            _seatService = new SeatService(_mockSeatRepository.Object);
            _seat = new Seat
            {
                Id = 1,
                Number = 1,
                Row = 1,
                AuditoriumId = 5
            };
        }

        [TestMethod]
        public void Get_Seats_By_Auditorium_Id_Return_List()
        {
            // Arrange
            _mockSeatRepository
                .Setup(x => x.GetSeatsByAuditoriumId(It.IsAny<int>()))
                .ReturnsAsync(new List<Seat>() { _seat });

            // Act
            var resultObject = _seatService
                .GetSeatsByAuditoriumId(_seat.Id)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.IsInstanceOfType(resultObject, typeof(SeatAuditoriumDomainModel));
            Assert.AreEqual(_seat.Id, resultObject.Seats[0].Id);
            Assert.IsNotNull(resultObject);
        }

        [TestMethod]
        public void Get_Seats_By_Auditorium_Id_Return_Null()
        {
            // Arrange
            _mockSeatRepository
                .Setup(x => x.GetSeatsByAuditoriumId(It.IsAny<int>()))
                .ReturnsAsync(new List<Seat>());

            // Act
            var resultObject = _seatService
                .GetSeatsByAuditoriumId(_seat.Id)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.IsNull(resultObject);
 
        }


    }
}
