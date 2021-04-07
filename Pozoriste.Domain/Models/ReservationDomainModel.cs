using System;
using System.Collections.Generic;
using System.Text;

namespace Pozoriste.Domain.Models
{
    public class ReservationDomainModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ShowId { get; set; }
        public List<SeatDomainModel> SeatsList { get; set; }

    }
}
