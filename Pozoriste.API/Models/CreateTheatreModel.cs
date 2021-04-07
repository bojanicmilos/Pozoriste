using Pozoriste.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pozoriste.API.Models
{
    public class CreateTheatreModel
    {
        [Required]
        [StringLength(50, ErrorMessage = Messages.ADDRESS_PROPERTIE_CITY_NAME_NOT_VALID)]
        public string CityName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = Messages.THEATRE_NAME_NOT_VALID)]
        public string Name { get; set; }

        [Required]
        [Range(1, 20, ErrorMessage = Messages.AUDITORIUM_PROPERTY_SEATROWSNUMBER_NOT_VALID)]
        public int SeatRows { get; set; }

        [Required]
        [Range(1, 20, ErrorMessage = Messages.AUDITORIUM_PROPERTY_SEATNUMBER_NOT_VALID)]
        public int NumberOfSeats { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = Messages.AUDITORIUM_PROPERTY_NAME_NOT_VALID)]
        public string AuditName { get; set; }
    }
}
