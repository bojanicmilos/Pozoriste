using System;
using System.Collections.Generic;
using System.Text;

namespace Pozoriste.Domain.Models
{
    public class UserReservationDomainModel
    {
        public int Id { get; set; }
        public string ShowTime { get; set; }
        public string AuditoriumName { get; set; }
        public string TheatreName { get; set; }
        public string PieceTitle { get; set; }
        public List<SeatDomainModel> ReservedSeats { get; set; }
    }
}
