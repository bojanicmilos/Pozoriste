using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pozoriste.Data.Entities;
using Pozoriste.Data.Enums;
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
    public class PieceServiceTests
    {
        private Mock<IPiecesRepository> _mockPieceRepository;
        private PieceService _pieceService;
        private Piece _piece;
        private PieceDomainModel _pieceDomainModel;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockPieceRepository = new Mock<IPiecesRepository>();
            _pieceService = new PieceService(_mockPieceRepository.Object);
            _piece = new Piece
            {
                Id = 1,
                Description = "komad",
                Genre = Genre.COMEDY,
                IsActive = true,
                Year = 1999,
            };

            _pieceDomainModel = new PieceDomainModel
            {
                Id = _piece.Id,
                Description = "opis",
                Genre = _piece.Genre,
                isActive = _piece.IsActive,
                Title = _piece.Title,
                Year = _piece.Year
            };
        }

        [TestMethod]
        public void AddPiece_Successful_Return_Piece_Domain_Model()
        {
            // Arrange
            _mockPieceRepository
                .Setup(x => x.Insert(It.IsAny<Piece>()))
                .Returns(_piece);

            // Act
            var resultObject = _pieceService
                .AddPiece(_pieceDomainModel)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.IsInstanceOfType(resultObject, typeof(PieceDomainModel));
            Assert.AreEqual(_piece.Description, resultObject.Description);
        }

        [TestMethod]
        public void AddPiece_NotSuccessful_Return_Null()
        {
            // Arrange
            _mockPieceRepository
                .Setup(x => x.Insert(It.IsAny<Piece>()))
                .Returns(null as Piece);

            // Act
            var resultObject = _pieceService
                .AddPiece(_pieceDomainModel)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.IsNull(resultObject);
        }

        [TestMethod]
        public void DeletePiece_Successful_ReturnPieceDomainModel()
        {
            // Arrange
            _mockPieceRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_piece);

            _mockPieceRepository
                .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(_piece);


            // Act
            var resultObject = _pieceService
                .DeletePiece(_piece.Id)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.IsInstanceOfType(resultObject, typeof(PieceDomainModel));
            Assert.IsNotNull(resultObject);
            Assert.AreEqual(_piece.Id, resultObject.Id);
        }


        // _pieceRepository.GetByIdAsync(id) // NOT NULL
        // _pieceRepository.Delete(id) // NULL
        [TestMethod]
        public void DeletePiece_Not_Successful_Return_Null_On_Delete()
        {
            // Arrange
            _mockPieceRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_piece);

            _mockPieceRepository
                .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(null as Piece);

            // Act
            var resultObject = _pieceService
                .DeletePiece(_piece.Id)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.IsNull(resultObject);
        }

        // _pieceRepository.GetByIdAsync(id) // NOT NULL
        [TestMethod]
        public void DeletePiece_NotSuccessful_Return_Null_On_Get_By_Id()
        {
            // Arrange
            _mockPieceRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(null as Piece);

            // Act
            var resultObject = _pieceService
                .DeletePiece(_piece.Id)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.IsNull(resultObject);
        }

        [TestMethod]
        public void Get_All_Pieces_ReturnList()
        {
            // Arrange
            int expectedCount = 1;

            _mockPieceRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Piece>() { _piece });

            // Act
            var objectList = _pieceService
                .GetAllPieces()
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.AreEqual(expectedCount, objectList.Count());
            Assert.IsNotNull(objectList);
        }

        [TestMethod]
        public void Get_All_Pieces_Return_Null()
        {
            // Arrange
            _mockPieceRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Piece>());

            // Act
            var nullObject = _pieceService
                .GetAllPieces()
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.IsNull(nullObject);
        }

        [TestMethod]
        public void Get_Active_Pieces_Return_List()
        {
            // Arrange
            int expectedCount = 1;

            _mockPieceRepository
                .Setup(x => x.GetActivePiecesAsync())
                .ReturnsAsync(new List<Piece>() { _piece});

            // Act
            var objectList = _pieceService
                .GetAllPieces(true)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.AreEqual(expectedCount, objectList.Count());
            Assert.IsNotNull(objectList);
            Assert.IsInstanceOfType(objectList, typeof(List<PieceDomainModel1>));
        }

        [TestMethod]
        public void Get_Active_Pieces_Return_Null()
        {
            // Arrange
            _mockPieceRepository
                .Setup(x => x.GetActivePiecesAsync())
                .ReturnsAsync(new List<Piece>());

            // Act
            var nullObject = _pieceService
                .GetAllPieces()
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.IsNull(nullObject);
        }

        [TestMethod]
        public void GetPiece_By_Id_Asyncc_Successful()
        {
            // Arrange
            _mockPieceRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_piece);

            // Act
            var resultObject = _pieceService
                .GetPieceByIdAsyncc(_piece.Id)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            // Arrange
            Assert.IsNotNull(resultObject);
            Assert.IsInstanceOfType(resultObject, typeof(PieceDomainModel));
            Assert.AreEqual(_piece.Id, resultObject.Id);
                
        }

        [TestMethod]
        public void GetPiece_By_Id_Asyncc_Not_Successfull_Return_Null()
        {
            // Arrange
            _mockPieceRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(null as Piece);

            // Act 
            var resultObject = _pieceService
                .GetPieceByIdAsyncc(_piece.Id)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.IsNull(resultObject);
        }


        //PieceDomainModel1 2 Tests
        [TestMethod]
        public void GetPiece_By_Id_Async_SuccessfulSecondDomainModel()
        {
            // Arrange
            _mockPieceRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_piece);

            // Act
            var resultObject = _pieceService
                .GetPieceByIdAsync(_piece.Id)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            // Arrange
            Assert.IsNotNull(resultObject);
            Assert.IsInstanceOfType(resultObject, typeof(PieceDomainModel1));
            Assert.AreEqual(_piece.Id, resultObject.Id);
        }

        [TestMethod]
        public void GetPiece_By_Id_Async_Not_Successfull_Return_Null()
        {
            // Arrange
            _mockPieceRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(null as Piece);

            // Act 
            var resultObject = _pieceService
                .GetPieceByIdAsync(_piece.Id)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.IsNull(resultObject);
        }

        [TestMethod]
        public void UpdatePiece_Successful_Return_DomainModel()
        {
            // Arrange
            _mockPieceRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_piece);

            _mockPieceRepository
                .Setup(x => x.Update(It.IsAny<Piece>()))
                .Returns(_piece);
                
                
            // Act 
            var resultObject = _pieceService
                .UpdatePiece(_pieceDomainModel)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.IsNotNull(resultObject);
        }

        [TestMethod]
        public void UpdatePiece_NotSuccessful()
        {
            // Arrange
            _mockPieceRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(null as Piece);

            // Act
            var resultObject = _pieceService
                .UpdatePiece(_pieceDomainModel)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.IsNull(resultObject);
        }

        [TestMethod]
        public void UpdatePiece_NotSuccessful_Update_Fail_Return_Null()
        {
            // Arrange
            _mockPieceRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_piece);

            _mockPieceRepository
                .Setup(x => x.Update(It.IsAny<Piece>()))
                .Returns(null as Piece);

            // Act
            var resultObject = _pieceService
                .UpdatePiece(_pieceDomainModel)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.IsNull(resultObject);
        }


    }
}
