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
    public interface IReservationsRepository : IRepository<Reservation>
    {
        Task<IEnumerable<Reservation>> GetReservationByShowId(int showId);
        Task<IEnumerable<Reservation>> GetReservationsByUserId(int userId);
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

        public async Task<IEnumerable<Reservation>> GetReservationByShowId(int showId)
        {
            var reservation = await _theatreContext.Reservations
                .Include(rs => rs.ReservationSeats)
                .ThenInclude(s => s.Seat)
                .ThenInclude(a => a.Auditorium)
                .Where(reservation => reservation.ShowId == showId)
                .ToListAsync();

            return reservation;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByUserId(int userId)
        {
            var data = await _theatreContext.Reservations
                .Include(show => show.Show)
                .ThenInclude(piece => piece.Piece)
                .Include(show => show.Show)
                .ThenInclude(auditorium => auditorium.Auditorium)
                .ThenInclude(theatre => theatre.Theatre)
                .Include(rs => rs.ReservationSeats)
                .ThenInclude(seat => seat.Seat)
                .Where(reservation => reservation.UserId == userId)
                .ToListAsync();

            return data;
        }
    }
}
