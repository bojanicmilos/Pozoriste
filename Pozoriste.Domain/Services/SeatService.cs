using Pozoriste.Domain.Interfaces;
using Pozoriste.Domain.Models;
using Pozoriste.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pozoriste.Domain.Services
{
    public class SeatService : ISeatService
    {
        private readonly ISeatsRepository _seatsRepository;

        public SeatService(ISeatsRepository seatsRepository)
        {
            _seatsRepository = seatsRepository;
        }

        public async Task<SeatAuditoriumDomainModel> GetSeatsByAuditoriumId(int auditoriumId)
        {
            var seatsInAuditorium = await _seatsRepository
                .GetSeatsByAuditoriumId(auditoriumId);

            if (seatsInAuditorium.Count() == 0)
            {
                return null;
            }

            int maxRow = seatsInAuditorium.Max(seat => seat.Row);
            int maxNumber = seatsInAuditorium.Max(seat => seat.Number);

            seatsInAuditorium = seatsInAuditorium.OrderBy(x => x.Row).ThenBy(x => x.Number);


            SeatAuditoriumDomainModel seatAuditoriumDomainModel = new SeatAuditoriumDomainModel
            {
                Seats = seatsInAuditorium.Select(seat => new SeatDomainModel
                {
                    Id = seat.Id,
                    Number = seat.Number,
                    Row = seat.Row,
                    AuditoriumId = seat.AuditoriumId
                }).ToList(),
                MaxRow = maxRow,
                MaxNumber = maxNumber
            };

            return seatAuditoriumDomainModel;
        }
    }
}
