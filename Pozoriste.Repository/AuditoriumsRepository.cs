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
    public interface IAuditoriumsRepository : IRepository<Auditorium>
    {
        Task<IEnumerable<Auditorium>> GetByAuditName(string name, int id);
        IEnumerable<Auditorium> GetAuditoriumsByCinemaId(int id);
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

        public async Task<IEnumerable<Auditorium>> GetAllAsync()
        {
            var data = await _theatreContext.Auditoriums.ToListAsync();

            return data;
        }

        public IEnumerable<Auditorium> GetAuditoriumsByCinemaId(int id)
        {
            var audits = _theatreContext.Auditoriums.Where(theatre => theatre.TheatreId == id);


            return audits;
        }

        public async Task<IEnumerable<Auditorium>> GetByAuditName(string name, int id)
        {
            var data = await _theatreContext.Auditoriums.Where(x => x.Name.Equals(name) && x.TheatreId.Equals(id)).ToListAsync();

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
