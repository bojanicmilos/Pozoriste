using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pozoriste.API.Controllers;
using Pozoriste.Domain.Interfaces;
using Pozoriste.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pozoriste.Tests.Controllers
{
    [TestClass]
    public class SeatsControllerTests
    {
        private Mock<ISeatService> _seatService;
        private Mock<IReservationService> _reservationService;

        [TestMethod]
        public void GetAll_Seats_By_Auditorium_Id()
        {
            //Arrange
            int expectedStatusCode = 200;
            int expectedRow = 5;
            int expectedNumber = 5;

            SeatAuditoriumDomainModel seatAuditoriumDomainModel = new SeatAuditoriumDomainModel
            {
                MaxNumber = 5,
                MaxRow = 5,
                Seats = new List<SeatDomainModel>()
            };

            Task<SeatAuditoriumDomainModel> responseTask = Task.FromResult(seatAuditoriumDomainModel);

            _seatService = new Mock<ISeatService>();
            _seatService.Setup(x => x.GetSeatsByAuditoriumId(It.IsAny<int>())).Returns(responseTask);
            SeatsController seatsController = new SeatsController(_seatService.Object);

            //Act
            var result = seatsController.GetSeatsByAuditoriumId(1).ConfigureAwait(false).GetAwaiter().GetResult().Result;
            var objectResult = ((OkObjectResult)result).Value;
            SeatAuditoriumDomainModel seatsResult = (SeatAuditoriumDomainModel)objectResult;

            //Assert
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(expectedRow, seatsResult.MaxRow);
            Assert.AreEqual(expectedNumber, seatsResult.MaxNumber);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(expectedStatusCode, ((OkObjectResult)result).StatusCode);
        }

        [TestMethod]
        public void GetAll_Seats_By_Auditorium_Id_Return_Not_Found()
        {
            //Arrange
            int expectedStatusCode = 404;

            SeatAuditoriumDomainModel seatAuditoriumDomainModel = null;

            Task<SeatAuditoriumDomainModel> responseTask = Task.FromResult(seatAuditoriumDomainModel);

            _seatService = new Mock<ISeatService>();
            _seatService.Setup(x => x.GetSeatsByAuditoriumId(It.IsAny<int>())).Returns(responseTask);
            SeatsController seatsController = new SeatsController(_seatService.Object);

            //Act
            var result = seatsController.GetSeatsByAuditoriumId(1).ConfigureAwait(false).GetAwaiter().GetResult().Result;
            var objectResult = ((NotFoundObjectResult)result).Value;
            string seatsResult = (string)objectResult;

            //Assert
            Assert.IsNotNull(objectResult);
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.AreEqual(expectedStatusCode, ((NotFoundObjectResult)result).StatusCode);
        }
    }
}
