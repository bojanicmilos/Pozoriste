using System;
using System.Collections.Generic;
using System.Text;

namespace Pozoriste.Data.Entities
{         
    public class Auditorium
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Show> Shows { get; set; }
        public ICollection<Seat> Seats { get; set; }
        public int TheatreId { get; set; }
        public Theatre Theatre { get; set; }
    }
}
