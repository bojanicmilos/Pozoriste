using Pozoriste.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pozoriste.API.Models
{
    public class ReservationModel
    {
        public int UserId { get; set; }
        public int ShowId { get; set; }
        public List<SeatDomainModel> SeatIds { get; set; }

    }
}
