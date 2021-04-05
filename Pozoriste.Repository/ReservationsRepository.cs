using Microsoft.EntityFrameworkCore;
using Pozoriste.Data.Context;
using Pozoriste.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pozoriste.Repository
{
    public interface IReservationsRepository : IRepository<Reservation>
    {

    }
    public class ReservationsRepository : IReservationsRepository
    {
        private TheatreContext _theatreContext;

        public ReservationsRepository(TheatreContext theatreContext)
        {
            _theatreContext = theatreContext;
        }

        public async Task<Reservation> Delete(int id)
        {
            Reservation existing = await _theatreContext.Reservations.FindAsync(id);
            var result = _theatreContext.Reservations.Remove(existing);

            return result.Entity;
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            var data = await _theatreContext.Reservations.ToListAsync();

            return data;
        }

        public async Task<Reservation> GetByIdAsync(int id)
        {
            var data = await _theatreContext.Reservations.FindAsync(id);

            return data;
        }

        public Reservation Insert(Reservation obj)
        {
            return _theatreContext.Reservations.Add(obj).Entity;
        }

        public void Save()
        {
            _theatreContext.SaveChanges();
        }

        public Reservation Update(Reservation obj)
        {
            _theatreContext.Entry(obj).State = EntityState.Modified;

            return obj;
        }
    }
}
