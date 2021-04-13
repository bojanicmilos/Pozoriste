using Pozoriste.Domain.Common;
using Pozoriste.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pozoriste.API.Models
{
    public class ShowModel
    {
        public DateTime ShowTime  { get; set; }
        [Required]
        [Range(100, 5000, ErrorMessage = Messages.TICKETS_INVALID_PRICE )]
        public double TicketPrice { get; set; }
        public int AuditoriumId { get; set; }
        public int PieceId { get; set; }
        public List<ActorDomainModel> ActorIds { get; set; }

    }
}
