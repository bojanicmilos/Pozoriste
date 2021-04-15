using Pozoriste.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pozoriste.Domain.Models
{
    public class ShowPieceActorAuditoriumTheatreDomainModel
    {
        public int Id { get; set; }
        public string ShowTime { get; set; }
        public double TicketPrice { get; set; }
        public string PieceTitle { get; set; }
        public string PieceDescription { get; set; }
        public int PieceYear { get; set; }
        public int PieceId { get; set; }
        public string Genre { get; set; }
        public string AuditoriumName { get; set; }
        public int AuditoriumId { get; set; }
        public string TheatreName { get; set; }
        public List<ActorDomainModel> Actors { get; set; }
    }
}
