using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pozoriste.Data.Entities
{
    [Table("address")]
    public class Address
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public string StreetName { get; set; }
        public Theatre Theatre { get; set; }
    }
}
