using Pozoriste.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pozoriste.Domain.Interfaces
{
    public interface IReservationService
    {
        Task<ReservationResultModel> CreateReservation(ReservationDomainModel requestedReservation);
        Task<IEnumerable<SeatDomainModel>> GetTakenSeats(int showId);
        Task<IEnumerable<UserReservationDomainModel>> GetReservationsByUserId(int userId);
    }
}
