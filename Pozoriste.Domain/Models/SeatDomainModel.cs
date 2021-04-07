using System;
using System.Collections.Generic;
using System.Text;

namespace Pozoriste.Domain.Models
{
    public class SeatDomainModel
    {
        public int Id { get; set; }
        public int AuditoriumId { get; set; }
        public int Row { get; set; }
        public int Number { get; set; }

    }
}