using Pozoriste.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pozoriste.Data.Entities
{
    public class Piece
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public bool IsActive { get; set; }
        public Genre Genre { get; set; }
        public ICollection<Show> Shows { get; set; }
    }
}
