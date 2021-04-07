using Pozoriste.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pozoriste.Domain.Models
{
    public class AddressDomainModel
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public string StreetName { get; set; }
        public Theatre Theatre { get; set; }
    }
}
