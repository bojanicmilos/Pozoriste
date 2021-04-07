using System;
using System.Collections.Generic;
using System.Text;

namespace Pozoriste.Domain.Models
{
    public class TheatreDomainModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AddressId { get; set; }
        public List<AuditoriumDomainModel> AuditoriumsList { get; set; }
    }
}
