using System;
using System.Collections.Generic;
using System.Text;

namespace Pozoriste.Domain.Models
{
    public class ShowResultModel
    {
        public bool isSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public ShowDomainModel ShowDomainModel { get; set; }


    }
}
