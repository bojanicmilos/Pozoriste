using System;
using System.Collections.Generic;
using System.Text;

namespace Pozoriste.Data.Entities
{
    public class Show
    {
        public int Id { get; set; }
        public DateTime ShowTime { get; set; }
        public double TicketPrice { get; set; }
        public int AuditoriumId { get; set; }
        public Auditorium Auditorium { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public int PieceId { get; set; }
        public Piece Piece { get; set; }
        public ICollection<ShowActor> ShowActors { get; set; }
    }
}
