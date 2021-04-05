using Pozoriste.Data.Enums;
using Pozoriste.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pozoriste.API.Models
{
    public class PieceModel
    {
        [Required]
        [StringLength(70, ErrorMessage = Messages.PIECE_PROPERTIE_TITLE_NOT_VALID)]
        public string Title { get; set; }

        [Required]
        [StringLength(450)]
        public string Description { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [Required]
        [Range(1000, 2050, ErrorMessage = Messages.PIECE_PROPERTIE_YEAR_NOT_VALID)]
        public int Year { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
