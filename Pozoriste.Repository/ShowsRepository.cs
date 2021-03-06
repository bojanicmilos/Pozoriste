using Microsoft.EntityFrameworkCore;
using Pozoriste.Data.Context;
using Pozoriste.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pozoriste.Repository
{
    public interface IShowsRepository : IRepository<Show>
    {
        Task<IEnumerable<Show>> GetByAuditoriumId(int auditoriumId);
        Task<IEnumerable<Show>> GetFutureShows();
        Task<IEnumerable<Show>> GetFutureShowsByPieceIdAsync(int pieceId);
    }
    public class ShowsRepository : IShowsRepository
    {
        private TheatreContext _theatreContext;

        public ShowsRepository(TheatreContext theatreContext)
        {
            _theatreContext = theatreContext;
        }

        public async Task<Show> Delete(int id)
        {
            Show existing = await _theatreContext.Shows.FindAsync(id);
            var result = _theatreContext.Shows.Remove(existing);

            return result.Entity;
        }

        public async Task<IEnumerable<Show>> GetAllAsync()
        {
            var data = await _theatreContext.Shows
                .Include(auditorium => auditorium.Auditorium)
                .ThenInclude(theatre => theatre.Theatre)
                .Include(piece => piece.Piece)
                .Include(showactor => showactor.ShowActors)
                .ThenInclude(actor => actor.Actor)
                .ToListAsync();

            return data;
        }

        public async Task<IEnumerable<Show>> GetByAuditoriumId(int auditoriumId)
        {
            var showsData = await _theatreContext.Shows
                .Include(x => x.Auditorium)
                .Where(x => x.AuditoriumId == auditoriumId).ToListAsync();

            return showsData;
        }

        public async Task<Show> GetByIdAsync(int id)
        {
            var data = await _theatreContext.Shows
                .Include(piece => piece.Piece)
                .Include(auditorium => auditorium.Auditorium)
                .ThenInclude(theatre => theatre.Theatre)
                .Include(showActor => showActor.ShowActors)
                .ThenInclude(actor => actor.Actor)
                .Include(reservation => reservation.Reservations)
                .FirstOrDefaultAsync(show => show.Id == id);

            return data;
        }

        public async Task<IEnumerable<Show>> GetFutureShows()
        {
            var shows = await _theatreContext.Shows
                .Include(x => x.Piece)
                .Include(x => x.Auditorium)
                .ThenInclude(x => x.Theatre)
                .Where(x => x.ShowTime.CompareTo(DateTime.Now) > 0).ToListAsync();

            return shows;
        }

        public async Task<IEnumerable<Show>> GetFutureShowsByPieceIdAsync(int pieceId)
        {
            var shows = await _theatreContext.Shows
                .Include(x => x.Auditorium)
                .ThenInclude(x => x.Theatre)
                .Include(x => x.Piece)
                .Where(x => x.ShowTime.CompareTo(DateTime.Now) > 0 && x.PieceId.Equals(pieceId)).ToListAsync();

            return shows;
        }

        public Show Insert(Show obj)
        {
            return _theatreContext.Shows.Add(obj).Entity;
        }

        public void Save()
        {
            _theatreContext.SaveChanges();
        }

        public Show Update(Show obj)
        {
            _theatreContext.Entry(obj).State = EntityState.Modified;

            return obj;
        }
    }
}
