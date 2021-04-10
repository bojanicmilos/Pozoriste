using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pozoriste.Data.Entities;
using Pozoriste.Domain.Common;
using Pozoriste.Domain.Models;
using Pozoriste.Domain.Services;
using Pozoriste.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pozoriste.Tests.Services
{
    [TestClass]
    public class ShowServiceTests
    {
        private Mock<IShowsRepository> _mockShowsRepository;
        private Mock<IAuditoriumsRepository> _mockAuditoriumsRepository;
        private Mock<IPiecesRepository> _mockPiecesRepository;
        private Mock<IActorsRepository> _mockActorsRepository;

        private ShowService _showService;
        private Show _show;
        private ShowDomainModel _showDomainModel;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockShowsRepository = new Mock<IShowsRepository>();
            _mockAuditoriumsRepository = new Mock<IAuditoriumsRepository>();
            _mockPiecesRepository = new Mock<IPiecesRepository>();
            _mockActorsRepository = new Mock<IActorsRepository>();
            _showService = new ShowService(
                _mockShowsRepository.Object
                , _mockAuditoriumsRepository.Object,
                _mockPiecesRepository.Object, _mockActorsRepository.Object);
        }


        [TestMethod]
        public void AddShow_NotSuccessful_Show_In_The_Past()
        {
            // Arrange
            _showDomainModel = new ShowDomainModel
            {
                ShowTime = DateTime.Now.AddDays(-5)
            };
            string expectedMessage = Messages.SHOW_IN_THE_PAST;

            // Act
            var resultObject = _showService
                .AddShow(_showDomainModel)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.IsFalse(resultObject.isSuccessful);
            Assert.AreEqual(resultObject.ErrorMessage, expectedMessage);
        }

        [TestMethod]
        public void AddShow_NotSuccessful_Auditorium_Does_Not_Exist()
        {
            // Arrange
            string expectedMessage = Messages.CANNOT_CREATE_SHOW_AUDITORIUM_DOES_NOT_EXIST;
            _showDomainModel = new ShowDomainModel
            {
                ShowTime = DateTime.Now.AddDays(5)
            };

            _mockAuditoriumsRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(null as Auditorium);

            // Act
            var resultObject = _showService
                .AddShow(_showDomainModel)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.IsFalse(resultObject.isSuccessful);
            Assert.AreEqual(resultObject.ErrorMessage, expectedMessage);
        }

        [TestMethod]
        public void AddShow_NotSuccessful_Piece_Does_Not_Exist()
        {
            // Arrange 
            string expectedMessage = Messages.CANNOT_CREATE_SHOW_PIECE_DOES_NOT_EXIST;
            _showDomainModel = new ShowDomainModel
            {
                ShowTime = DateTime.Now.AddDays(5)
            };

            _mockAuditoriumsRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Auditorium());

            _mockPiecesRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(null as Piece);

            // Act
            var resultObject = _showService
                .AddShow(_showDomainModel)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();


            // Assert
            Assert.IsFalse(resultObject.isSuccessful);
            Assert.AreEqual(resultObject.ErrorMessage, expectedMessage);
        }
    }
}
