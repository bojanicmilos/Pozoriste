using Microsoft.EntityFrameworkCore;
using Pozoriste.Data.Context;
using Pozoriste.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pozoriste.Repository
{
    public interface ITheatreRepository : IRepository<Theatre> { }

    public class TheatresRepository : ITheatreRepository
    {
        private TheatreContext _theatreContext;

        public TheatresRepository(TheatreContext theatreContext)
        {
            _theatreContext = theatreContext;
        }

        public async Task<Theatre> Delete(int id)
        {
            Theatre existing = await _theatreContext.Theatres.FindAsync(id);
            var result = _theatreContext.Theatres.Remove(existing);

            return result.Entity;
        }

        public async Task<IEnumerable<Theatre>> GetAllAsync()
        {
            var data = await _theatreContext.Theatres.Include(x => x.Auditoriums).ToListAsync();

            return data;
        }

        public async Task<Theatre> GetByIdAsync(int id)
        {
            var data = await _theatreContext.Theatres.FindAsync(id);

            return data;
        }

        public Theatre Insert(Theatre obj)
        {
            return _theatreContext.Theatres.Add(obj).Entity;
        }

        public void Save()
        {
            _theatreContext.SaveChanges();
        }

        public Theatre Update(Theatre obj)
        {
            _theatreContext.Entry(obj).State = EntityState.Modified;

            return obj;
        }
    }
}
