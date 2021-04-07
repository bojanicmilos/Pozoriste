using System;
using System.Collections.Generic;
using System.Text;

namespace Pozoriste.Domain.Models
{
    public class ShowDomainModel
    {
        public int Id { get; set; }
        public DateTime ShowTime { get; set; }
        public double TicketPrice { get; set; }
        public int AuditoriumId { get; set; }
        public int PieceId { get; set; }
        public List<ActorDomainModel> ActorsList { get; set; }

    }
}
