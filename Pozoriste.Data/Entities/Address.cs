using System;
using System.Collections.Generic;
using System.Text;

namespace Pozoriste.Data.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public string StreetName { get; set; }
        public Theatre Theatre { get; set; }
    }
}
