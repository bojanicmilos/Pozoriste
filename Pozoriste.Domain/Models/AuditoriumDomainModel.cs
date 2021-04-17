using System;
using System.Collections.Generic;
using System.Text;

namespace Pozoriste.Domain.Models
{
    public class AuditoriumDomainModel
    {
        public int Id { get; set; }
        public int TheatreId { get; set; }
        public string TheatreName { get; set; }
        public string Name { get; set; }
        public List<SeatDomainModel> SeatsList { get; set; }
    }
}
