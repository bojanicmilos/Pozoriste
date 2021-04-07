using Microsoft.EntityFrameworkCore;
using Pozoriste.Data.Context;
using Pozoriste.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pozoriste.Repository
{
    public interface IShowsRepository : IRepository<Show>
    {

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

        public async Task<Show> GetByIdAsync(int id)
        {
            var data = await _theatreContext.Shows
                .Include(showActor => showActor.ShowActors)
                .ThenInclude(actor => actor.Actor)
                .FirstOrDefaultAsync(show => show.Id == id);

            return data;
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
