using System;
using System.Collections.Generic;
using System.Text;

namespace Pozoriste.Domain.Common
{
    public static class Messages
    {
        #region Pieces
        public const string PIECE_DOES_NOT_EXIST = "Piece does not exist.";
        public const string PIECE_PROPERTIE_TITLE_NOT_VALID = "The piece title cannot be longer than 70 characters.";
        public const string PIECE_PROPERTIE_YEAR_NOT_VALID = "The piece year must be between 1000-2050.";
        public const string PIECE_CREATION_ERROR = "Error occured while creating new piece, please try again.";
        #endregion
    }
}
