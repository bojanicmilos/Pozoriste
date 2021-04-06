using Pozoriste.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pozoriste.API.Models
{
    public class ActorModel
    {
        [Required]
        [StringLength(50, ErrorMessage = Messages.ACTOR_CREATION_ERROR)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = Messages.ACTOR_CREATION_ERROR)]
        public string LastName { get; set; }
    }
}
