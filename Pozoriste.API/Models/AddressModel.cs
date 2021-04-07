using Pozoriste.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pozoriste.API.Models
{
    public class AddressModel
    {
        [Required]
        [StringLength(50, ErrorMessage = Messages.ADDRESS_PROPERTIE_CITY_NAME_NOT_VALID)]
        public string CityName { get; set; }

        [Required]
        [StringLength(80, ErrorMessage = Messages.ADDRESS_PROPERTIE_STREET_NAME_NOT_VALID)]
        public string StreetName { get; set; }

    }
}
