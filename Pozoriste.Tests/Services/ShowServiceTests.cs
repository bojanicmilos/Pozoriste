using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pozoriste.Data.Entities;
using Pozoriste.Domain.Common;
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
            Assert.AreEqual(expectedMessage, resultObject.ErrorMessage);
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
            Assert.AreEqual(expectedMessage, resultObject.ErrorMessage);
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
            Assert.AreEqual(expectedMessage, resultObject.ErrorMessage);
        }

        [TestMethod]
        public void AddShow_NotSuccessful_Shows_At_The_Same_Time_Error()
        {
            // Arrange
            string expectedMessage = Messages.SHOWS_AT_THE_SAME_TIME;
            bool expectedBool = false;

            _showDomainModel = new ShowDomainModel
            {
                ShowTime = DateTime.Now.AddMinutes(5)
            };

            _mockAuditoriumsRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Auditorium());

            _mockPiecesRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Piece());

            _mockShowsRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Show>() { new Show { ShowTime = DateTime.Now.AddHours(-1) } });

            // Act
            var objectResult = _showService
                .AddShow(_showDomainModel)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(expectedMessage, objectResult.ErrorMessage);
            Assert.AreEqual(expectedBool, objectResult.isSuccessful);
        }

        [TestMethod]
        public void Add_Show_NotSuccessful_Actors_Does_Not_Exist_In_DataBase()
        {
            // Arrange
            string expectedMessage = Messages.CANNOT_CREATE_SHOW_ACTORS_DOES_NOT_EXIST;
            bool expectedBool = false;

            _showDomainModel = new ShowDomainModel
            {
                ShowTime = DateTime.Now.AddMinutes(5),
                ActorsList = new List<ActorDomainModel>
                {
                    new ActorDomainModel
                    {
                        Id = 1
                    },
                    new ActorDomainModel
                    {
                        Id = 2
                    }
                }
            };

            _mockAuditoriumsRepository
               .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(new Auditorium());

            _mockPiecesRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Piece());

            _mockShowsRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Show>() { new Show { ShowTime = DateTime.Now.AddHours(5) } });

            _mockActorsRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Actor>()
                {
                    new Actor
                    {
                        Id = 2
                    },
                    new Actor
                    {
                        Id = 3
                    }
                });

            // Act 
            var objectResult = _showService
                .AddShow(_showDomainModel)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(expectedMessage, objectResult.ErrorMessage);
            Assert.IsInstanceOfType(objectResult, typeof(ShowResultModel));
            Assert.AreEqual(expectedBool, objectResult.isSuccessful);
        }

        [TestMethod]
        public void Add_Show_NotSuccessful_Some_Actors_Have_Two_Shows_On_Same_Day()
        {
            // Arrange
            string expectedMessage = Messages
                .CANNOT_CREATE_SHOW_SOME_ACTORS_HAVE_MORE_THAN_TWO_SHOWS_PER_DAY;
            bool expectedBool = false;

            _showDomainModel = new ShowDomainModel
            {
                ShowTime = DateTime.Now.AddMinutes(5),
                ActorsList = new List<ActorDomainModel>
                {
                    new ActorDomainModel
                    {
                        Id = 1
                    },
                    new ActorDomainModel
                    {
                        Id = 2
                    }
                }
            };

            _mockAuditoriumsRepository
               .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(new Auditorium());

            _mockPiecesRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Piece());

            _mockShowsRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Show>() { new Show { ShowTime = DateTime.Now.AddHours(5) } });

            _mockActorsRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Actor>()
                {
                    new Actor
                    {
                        Id = 1
                    },
                    new Actor
                    {
                        Id = 2
                    }
                });

            _mockActorsRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Actor
                {
                    ShowActors = new List<ShowActor>()
                {
                    new ShowActor
                    {
                        Show = new Show
                        {
                            ShowTime = DateTime.Now
                        }
                    },
                    new ShowActor
                    {
                         Show = new Show
                        {
                            ShowTime = DateTime.Now
                        }
                    },
                    new ShowActor
                    {
                        Show = new Show
                        {
                            ShowTime = DateTime.Now
                        }
                    }
                }
                });

            // Act 
            var objectResult = _showService
                .AddShow(_showDomainModel)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(expectedBool, objectResult.isSuccessful);
            Assert.AreEqual(expectedMessage, objectResult.ErrorMessage);
        }

        [TestMethod]
        public void AddShow_Successful_Insert_In_Database()
        {
            // Arrange
            string expectedMessage = null;
            bool expectedBool = true;

            _showDomainModel = new ShowDomainModel
            {
                ShowTime = DateTime.Now.AddMinutes(5),
                ActorsList = new List<ActorDomainModel>
                {
                    new ActorDomainModel
                    {
                        Id = 1
                    },
                    new ActorDomainModel
                    {
                        Id = 2
                    }
                }
            };

            _mockAuditoriumsRepository
               .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(new Auditorium());

            _mockPiecesRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Piece());

            _mockShowsRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Show>() { new Show { ShowTime = DateTime.Now.AddHours(5) } });

            _mockActorsRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Actor>()
                {
                    new Actor
                    {
                        Id = 1
                    },
                    new Actor
                    {
                        Id = 2
                    }
                });

            _mockActorsRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Actor
                {
                    ShowActors = new List<ShowActor>()
                {
                    new ShowActor
                    {
                        Show = new Show
                        {
                            ShowTime = DateTime.Now
                        }
                    },
                    new ShowActor
                    {
                         Show = new Show
                        {
                            ShowTime = DateTime.Now
                        }
                    }
                }
                });

            _show = new Show
            {
                Id = 1,
                ShowTime = DateTime.Now,
                TicketPrice = 20,
                ShowActors = new List<ShowActor>
                {
                    new ShowActor
                    {
                        Actor = new Actor
                        {
                            Id = 1,
                            FirstName = "ime",
                            LastName = "prezime",
                            ShowActors = new List<ShowActor>()
                        }
                    }
                }
            };

            _mockShowsRepository
                .Setup(x => x.Insert(It.IsAny<Show>()))
                .Returns(new Show { Id = 1, ShowTime = DateTime.Now});

            _mockShowsRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_show);

            // Act 
            var objectResult = _showService
                .AddShow(_showDomainModel)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.AreEqual(expectedBool, objectResult.isSuccessful);
            Assert.IsNull(expectedMessage, objectResult.ErrorMessage);
            Assert.IsNotNull(objectResult.ShowDomainModel);
        }

        [TestMethod]
        public void Get_All_Shows_Async_Return_List()
        {
            // Arrange
            int expectedCount = 2;
            _mockShowsRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Show>() 
                {
                    new Show
                    {
                        Id = 1,
                        Piece = new Piece
                        {
                            Id = 1,
                            IsActive = true,
                            Title = "naslov"
                        },
                        Auditorium = new Auditorium()
                        { Theatre = new Theatre() },
                        ShowTime = DateTime.Now.AddDays(1),
                        ShowActors = new List<ShowActor>()
                        {
                            new ShowActor
                            {
                                Actor = new Actor()
                                {
                                    FirstName = "ime"
                                }
                            }
                        }
                    },
                    new Show
                    {
                        Id = 1,
                        Piece = new Piece
                        {
                            Id = 1,
                            IsActive = true,
                            Title = "naslov"
                        },
                        Auditorium = new Auditorium()
                        { Theatre = new Theatre() },
                        ShowTime = DateTime.Now.AddDays(1),
                        ShowActors = new List<ShowActor>()
                        {
                            new ShowActor
                            {
                                Actor = new Actor()
                                {
                                    FirstName = "ime"
                                }
                            }
                        }
                    }
                });

            // Act
            var objectList = _showService
                .GetAllShowsAsync()
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.IsNotNull(objectList);
            Assert.AreEqual(expectedCount, objectList.Count());
            Assert.IsInstanceOfType(objectList,
                typeof(IEnumerable<ShowPieceActorAuditoriumTheatreDomainModel>));
        }

        [TestMethod]
        public void Get_All_Shows_Async_Return_Empty_List()
        {
            // Arrange
            _mockShowsRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Show>());

            //Act
            var objectList = _showService
                .GetAllShowsAsync()
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.IsNull(objectList);
        }


    }
}
