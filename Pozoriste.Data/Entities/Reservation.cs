using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pozoriste.Data.Entities
{
    [Table("reservation")]
    public class Reservation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ShowId { get; set; }
        public Show Show { get; set; }
        public ICollection<ReservationSeat> ReservationSeats { get; set; }
    }
}
