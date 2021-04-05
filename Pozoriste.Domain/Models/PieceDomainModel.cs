using Pozoriste.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pozoriste.Domain.Models
{
    public class PieceDomainModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public bool isActive { get; set; }
        public Genre Genre { get; set; }
    }
}
