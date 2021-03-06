using Pozoriste.Data.Entities;
using Pozoriste.Domain.Common;
using Pozoriste.Domain.Interfaces;
using Pozoriste.Domain.Models;
using Pozoriste.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pozoriste.Domain.Services
{
    public class ShowService : IShowService
    {
        private readonly IShowsRepository _showsRepository;
        private readonly IAuditoriumsRepository _auditoriumsRepository;
        private readonly IPiecesRepository _piecesRepository;
        private readonly IActorsRepository _actorsRepository;

        public ShowService(IShowsRepository showsRepository,
            IAuditoriumsRepository auditoriumsRepository,
            IPiecesRepository piecesRepository,
            IActorsRepository actorsRepository)
        {
            _showsRepository = showsRepository;
            _auditoriumsRepository = auditoriumsRepository;
            _piecesRepository = piecesRepository;
            _actorsRepository = actorsRepository;
        }

        public async Task<ShowResultModel> AddShow(ShowDomainModel requestedShow)
        {
            // check if requested show time is in the past
            if (requestedShow.ShowTime < DateTime.Now)
            {
                return new ShowResultModel
                {
                    isSuccessful = false,
                    ErrorMessage = Messages.SHOW_IN_THE_PAST
                };
            }

            // check if auditorium exist 
            var existingAuditorium = await _auditoriumsRepository.GetByIdAsync(requestedShow.AuditoriumId);

            if (existingAuditorium == null)
            {
                return new ShowResultModel
                {
                    isSuccessful = false,
                    ErrorMessage = Messages.CANNOT_CREATE_SHOW_AUDITORIUM_DOES_NOT_EXIST,
                };
            }

            // check if piece exist 
            var existingPiece = await _piecesRepository.GetByIdAsync(requestedShow.PieceId);

            if (existingPiece == null)
            {
                return new ShowResultModel
                {
                    isSuccessful = false,
                    ErrorMessage = Messages.CANNOT_CREATE_SHOW_PIECE_DOES_NOT_EXIST
                };
            }

            // check if shows are at the same time
            int showTime = 3;
            var allShows = await _showsRepository.GetAllAsync();
            var allShowsInAuditorium = allShows
                .Where(show => show.AuditoriumId == requestedShow.AuditoriumId);

            var showsAtTheSameTime = allShowsInAuditorium
                .Where(x => x.ShowTime < requestedShow.ShowTime.AddHours(showTime) && x.ShowTime > requestedShow.ShowTime.AddHours(-showTime));

            if (showsAtTheSameTime.Count() > 0)
            {
                return new ShowResultModel
                {
                    ErrorMessage = Messages.SHOWS_AT_THE_SAME_TIME,
                    isSuccessful = false
                };
            }

            // check if actors are provided 
            if (requestedShow.ActorsList.Count() == 0)
            {
                return new ShowResultModel
                {
                    ErrorMessage = Messages.ACTORS_NOT_PROVIDED,
                    isSuccessful = false
                };
            }

            // check if actors exist in database
            var allActors = await _actorsRepository.GetAllAsync();

            IEnumerable<int> actorIds = allActors.Select(actor => actor.Id);
            IEnumerable<int> requestedActorIds = requestedShow.ActorsList.Select(actor => actor.Id);

            var actorsInBoth = actorIds.Intersect(requestedActorIds);

            if (actorsInBoth.Count() < requestedActorIds.Count())
            {
                return new ShowResultModel
                {
                    isSuccessful = false,
                    ErrorMessage = Messages.CANNOT_CREATE_SHOW_ACTORS_DOES_NOT_EXIST
                };
            }

            //check if any actor has more than 2 shows on the same day
            var requestedActorsInDatabase = new List<Actor>();

            foreach (var requested in requestedShow.ActorsList)
            {
                var actor = await _actorsRepository.GetByIdAsync(requested.Id);

                requestedActorsInDatabase.Add(actor);
            }

            foreach (var actor in requestedActorsInDatabase)
            {
                if (actor.ShowActors.Any(showactor => showactor.Show.ShowTime.Date == requestedShow.ShowTime.Date))
                {
                    if (actor.ShowActors.Count(showActor => showActor.Show.ShowTime.Date == requestedShow.ShowTime.Date) > 2)
                    {
                        return new ShowResultModel
                        {
                            ErrorMessage = Messages
                            .CANNOT_CREATE_SHOW_SOME_ACTORS_HAVE_MORE_THAN_TWO_SHOWS_PER_DAY,
                            isSuccessful = false,
                        };
                    }

                }
            }

            // insert in database
            Show showToInsert = new Show
            {
                ShowTime = requestedShow.ShowTime,
                AuditoriumId = requestedShow.AuditoriumId,
                PieceId = requestedShow.PieceId,
                TicketPrice = requestedShow.TicketPrice
            };

            var insertedShow = _showsRepository.Insert(showToInsert);

            insertedShow.ShowActors = requestedShow.ActorsList.Select(actorDomainModel => new ShowActor
            {
                ActorId = actorDomainModel.Id,
                ShowId = insertedShow.Id

            }).ToList();

            _showsRepository.Save();

            // return created show to user
            var createdShow = await _showsRepository.GetByIdAsync(insertedShow.Id);

            ShowResultModel resultModel = new ShowResultModel
            {
                ErrorMessage = null,
                isSuccessful = true,
                ShowDomainModel = new ShowDomainModel
                {
                    Id = createdShow.Id,
                    AuditoriumId = createdShow.AuditoriumId,
                    PieceId = createdShow.PieceId,
                    TicketPrice = createdShow.TicketPrice,
                    ShowTime = createdShow.ShowTime,
                    PieceTitle = createdShow.Piece.Title,
                    ActorsList = createdShow.ShowActors.Select(showActor => new ActorDomainModel
                    {
                        Id = showActor.Actor.Id,
                        FirstName = showActor.Actor.FirstName,
                        LastName = showActor.Actor.LastName
                    }).ToList()
                }
            };

            return resultModel;
        }

        public async Task<ShowDomainModel> DeleteShow(int id)
        {
            var existingShow = await _showsRepository.GetByIdAsync(id);

            if (existingShow == null)
            {
                return null;
            }

            if (existingShow.Reservations.Any())
            {
                return null;
            }

            var deletedShow = await _showsRepository.Delete(id);

            _showsRepository.Save();

            return new ShowDomainModel
            {
                Id = deletedShow.Id,
                ShowTime = deletedShow.ShowTime,
                TicketPrice = deletedShow.TicketPrice
            };
        }

        public async Task<IEnumerable<ShowPieceActorAuditoriumTheatreDomainModel>> GetAllShowsAsync(string search = "")
        {
            var shows = await _showsRepository.GetAllAsync();

            if (shows.Count() == 0)
            {
                return null;
            }

            //shows for current pieces with at least 1 actor and in future
            shows = shows
                .Where(show => show.Piece.IsActive && show.ShowActors.Any() && show.ShowTime > DateTime.Now);

            if (search != "")
            {
                shows = shows
               .Where(show => show.TicketPrice.ToString().Contains(search) || show.ShowTime.ToString("MM/dd/yyyy HH:mm").Contains(search) || 
                 show.Piece.Title.ToLower().Contains(search.ToLower()) ||
                 show.Piece.Year.ToString().Contains(search) ||
                 show.ShowActors.Any(showActor =>           (showActor.Actor.FirstName.ToLower() + " " + showActor.Actor.LastName.ToLower()).Contains(search.ToLower())) ||
                 show.Auditorium.Theatre.Name.ToLower().Contains(search.ToLower()) ||
                 show.Auditorium.Name.ToLower().Contains(search.ToLower()) ||
                 (show.Piece.Genre.ToString() == "DRAMA" ? "drama" : show.Piece.Genre.ToString() == "COMEDY" ? "komedija" : show.Piece.Genre.ToString() == "TRAGEDY" ? "tragedija" : "").Contains(search.ToLower())
                    );
                 
            }



            IEnumerable<ShowPieceActorAuditoriumTheatreDomainModel> domainModels =
                shows.Select(show => new ShowPieceActorAuditoriumTheatreDomainModel
                {
                    Id = show.Id,
                    AuditoriumName = show.Auditorium.Name,
                    PieceDescription = show.Piece.Description,
                    PieceTitle = show.Piece.Title,
                    PieceYear = show.Piece.Year,
                    PieceId = show.Piece.Id,
                    AuditoriumId = show.Auditorium.Id,
                    Genre = show.Piece.Genre.ToString() == "COMEDY" ? "Komedija"
                    : show.Piece.Genre.ToString() == "DRAMA" ? "Drama" : show.Piece.Genre.ToString() == "TRAGEDY" ? "Tragedija" : "",
                    ShowTime = show.ShowTime.ToString("MM/dd/yyyy HH:mm"),
                    TheatreName = show.Auditorium.Theatre.Name,
                    TicketPrice = show.TicketPrice,
                    Actors = show.ShowActors.Select(showActor => new ActorDomainModel
                    {
                        Id = showActor.Actor.Id,
                        FirstName = showActor.Actor.FirstName,
                        LastName = showActor.Actor.LastName
                    }).ToList()
                });

            return domainModels;
        }

        public async Task<IEnumerable<ShowDomainModel>> GetFutureShows()
        {
            var shows = await _showsRepository.GetFutureShows();

            return shows.Select(show => new ShowDomainModel()
            {
                Id = show.Id,
                AuditoriumId = show.AuditoriumId,
                PieceId = show.PieceId,
                ShowTime = show.ShowTime,
                TicketPrice = show.TicketPrice
            });
        }

        public async Task<IEnumerable<ShowDomainModel>> GetFutureShowsByPieceId(int id)
        {
            var shows = await _showsRepository.GetFutureShowsByPieceIdAsync(id);

            if (shows == null)
            {
                return null;
            }

            var showsList = shows.Select(show => new ShowDomainModel
            {
                Id = show.Id,
                AuditoriumId = show.AuditoriumId,
                PieceId = show.PieceId,
                ShowTime = show.ShowTime,
                TicketPrice = show.TicketPrice
            });

            return showsList;
        }

        public async Task<IEnumerable<ShowDomainModel>> GetFutureShowsByPieceId(PieceDomainModel domainModel)
        {
            var shows = await _showsRepository.GetFutureShowsByPieceIdAsync(domainModel.Id);

            if (shows == null)
            {
                return null;
            }

            var showsList = shows.Select(show => new ShowDomainModel
            {
                Id = show.Id,
                AuditoriumId = show.AuditoriumId,
                PieceId = show.PieceId,
                ShowTime = show.ShowTime,
                TicketPrice = show.TicketPrice
            });

            return showsList;
        }

        public async Task<ShowPieceActorAuditoriumTheatreDomainModel> GetShowByIdAsync(int id)
        {
            var show = await _showsRepository.GetByIdAsync(id);

            if (show == null)
            {
                return null;
            }

            return new ShowPieceActorAuditoriumTheatreDomainModel
            {
                Id = show.Id,
                AuditoriumName = show.Auditorium.Name,
                PieceDescription = show.Piece.Description,
                PieceTitle = show.Piece.Title,
                PieceYear = show.Piece.Year,
                PieceId = show.Piece.Id,
                AuditoriumId = show.Auditorium.Id,
                Genre = show.Piece.Genre.ToString() == "COMEDY" ? "Komedija"
                    : show.Piece.Genre.ToString() == "DRAMA" ? "Drama" : show.Piece.Genre.ToString() == "TRAGEDY" ? "Tragedija" : "",
                ShowTime = show.ShowTime.ToString("MM/dd/yyyy HH:mm"),
                TheatreName = show.Auditorium.Theatre.Name,
                TicketPrice = show.TicketPrice,
                Actors = show.ShowActors.Select(showActor => new ActorDomainModel
                {
                    Id = showActor.Actor.Id,
                    FirstName = showActor.Actor.FirstName,
                    LastName = showActor.Actor.LastName
                }).ToList()
            };
        }
    }
}
