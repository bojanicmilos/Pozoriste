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

        public ShowService(IShowsRepository showsRepository)
        {
            _showsRepository = showsRepository;
        }

        public async Task<IEnumerable<ShowPieceActorAuditoriumTheatreDomainModel>> GetAllShowsAsync()
        {
            var shows = await _showsRepository.GetAll();

            if (shows.Count() == 0)
            {
                return null;
            }

            //shows for current pieces and with at least 1 actor
            shows = shows
                .Where(show => show.Piece.IsActive && show.ShowActors.Any());

            IEnumerable<ShowPieceActorAuditoriumTheatreDomainModel> domainModels =
                shows.Select(show => new ShowPieceActorAuditoriumTheatreDomainModel
                {
                    Id = show.Id,
                    AuditoriumName = show.Auditorium.Name,
                    PieceDescription = show.Piece.Description,
                    PieceTitle = show.Piece.Title,
                    PieceYear = show.Piece.Year,
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
    }
}
