using System;
using System.Collections.Generic;
using System.Text;

namespace Pozoriste.Domain.Models
{
    public class ReservationResultModel
    {
        public bool isSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public ReservationDomainModel ReservationDomainModel { get; set; }
    }
}
