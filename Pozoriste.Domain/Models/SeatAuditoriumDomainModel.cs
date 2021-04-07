using System;
using System.Collections.Generic;
using System.Text;

namespace Pozoriste.Domain.Models
{
    public class SeatAuditoriumDomainModel
    {
        public List<SeatDomainModel> Seats { get; set; }
        public int MaxRow { get; set; }
        public int MaxNumber { get; set; }
    }
}
