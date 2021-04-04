using Microsoft.EntityFrameworkCore;
using Pozoriste.Data.Context;
using Pozoriste.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pozoriste.Repository
{
    public interface IActorsRepository : IRepository<Actor>
    {

    }
    public class ActorsRepository : IActorsRepository
    {
        private TheatreContext _theatreContext;

        public ActorsRepository(TheatreContext theatreContext)
        {
            _theatreContext = theatreContext;
        }

        public async Task<Actor> Delete(int id)
        {
            Actor existing = await _theatreContext.Actors.FindAsync(id);
            var result = _theatreContext.Actors.Remove(existing);

            return result.Entity;
        }

        public async Task<IEnumerable<Actor>> GetAll()
        {
            var data = await _theatreContext.Actors.ToListAsync();

            return data;
        }

        public async Task<Actor> GetByIdAsync(int id)
        {
            var data = await _theatreContext.Actors.FindAsync(id);

            return data;
        }

        public Actor Insert(Actor obj)
        {
            return _theatreContext.Actors.Add(obj).Entity;
        }

        public Actor Update(Actor obj)
        {
            _theatreContext.Entry(obj).State = EntityState.Modified;

            return obj;
        }

        public void Save()
        {
            _theatreContext.SaveChanges();
        }
    }
}
