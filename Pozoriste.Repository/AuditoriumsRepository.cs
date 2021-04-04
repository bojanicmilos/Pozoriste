using Microsoft.EntityFrameworkCore;
using Pozoriste.Data.Context;
using Pozoriste.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pozoriste.Repository
{
    public interface IAuditoriumsRepository : IRepository<Auditorium>
    {

    }
    public class AuditoriumsRepository : IAuditoriumsRepository
    {
        private TheatreContext _theatreContext;

        public AuditoriumsRepository(TheatreContext theatreContext)
        {
            _theatreContext = theatreContext;
        }

        public async Task<Auditorium> Delete(int id)
        {
            Auditorium existing = await _theatreContext.Auditoriums.FindAsync(id);
            var result = _theatreContext.Auditoriums.Remove(existing);

            return result.Entity;
        }

        public async Task<IEnumerable<Auditorium>> GetAll()
        {
            var data = await _theatreContext.Auditoriums.ToListAsync();

            return data;
        }

        public async Task<Auditorium> GetByIdAsync(int id)
        {
            var data = await _theatreContext.Auditoriums.FindAsync(id);

            return data;
        }

        public Auditorium Insert(Auditorium obj)
        {
            return _theatreContext.Auditoriums.Add(obj).Entity;
        }

        public void Save()
        {
            _theatreContext.SaveChanges();
        }

        public Auditorium Update(Auditorium obj)
        {
            _theatreContext.Entry(obj).State = EntityState.Modified;

            return obj;
        }
    }
}
