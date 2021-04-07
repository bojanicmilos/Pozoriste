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
    public interface ISeatsRepository : IRepository<Seat> 
    {
        Task<IEnumerable<Seat>> GetSeatsByAuditoriumId(int auditoriumId);
    }
    public class SeatsRepository : ISeatsRepository
    {
        private TheatreContext _theatreContext;

        public SeatsRepository(TheatreContext theatreContext)
        {
            _theatreContext = theatreContext;
        }

        public async Task<Seat> Delete(int id)
        {
            Seat existing = await _theatreContext.Seats.FindAsync(id);
            var result = _theatreContext.Seats.Remove(existing);

            return result.Entity;
        }

        public async Task<IEnumerable<Seat>> GetAllAsync()
        {
            var data = await _theatreContext.Seats
                .Include(x => x.ReservationSeats)
                .ToListAsync();

            return data;
        }

        public async Task<Seat> GetByIdAsync(int id)
        {
            var data = await _theatreContext.Seats.FindAsync(id);

            return data;
        }

        public async Task<IEnumerable<Seat>> GetSeatsByAuditoriumId(int auditoriumId)
        {
            var seats = await _theatreContext.Seats
                .Where(seat => seat.AuditoriumId == auditoriumId)
                .ToListAsync();

            return seats;
        }

        public Seat Insert(Seat obj)
        {
            return _theatreContext.Seats.Add(obj).Entity;
        }

        public void Save()
        {
            _theatreContext.SaveChanges();
        }

        public Seat Update(Seat obj)
        {
            _theatreContext.Entry(obj).State = EntityState.Modified;

            return obj;
        }
    }
}
