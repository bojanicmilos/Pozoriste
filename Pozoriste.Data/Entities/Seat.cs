using System;
using System.Collections.Generic;
using System.Text;

namespace Pozoriste.Data.Entities
{
    public class Seat
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public int Number { get; set; }
        public int AuditoriumId { get; set; }
        public Auditorium Auditorium { get; set; }
        public ICollection<ReservationSeat> ReservationSeats { get; set; }
    }

}
