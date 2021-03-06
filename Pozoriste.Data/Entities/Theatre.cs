using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pozoriste.Data.Entities
{
    [Table("theatre")]
    public class Theatre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Auditorium> Auditoriums { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
