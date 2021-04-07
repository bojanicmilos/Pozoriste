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
        #region Actors
        public const string ACTOR_CREATION_ERROR = "Error while creating new actor, please try again.";
        public const string ACTOR_DOES_NOT_EXIST = "Actor does not exist.";
        #endregion
        #region Shows
        public const string SHOWS_AT_THE_SAME_TIME = "Doslo je do greske. Predstave u trazenom terminu vec postoje u auditoriujumu. ";
        public const string ACTOR_HAS_MORE_THAN_TWO_SHOWS_PER_DAY_ERROR = "Glumac ne moze da glumi u vise od dve predstave na dan.";
        public const string CANNOT_CREATE_SHOW_AUDITORIUM_DOES_NOT_EXIST = "Greska prilikom dodavanja predstave, auditorium ne postoji.";
        public const string CANNOT_CREATE_SHOW_PIECE_DOES_NOT_EXIST = "Pozorisni komad je nevazeci.";
        public const string CANNOT_CREATE_SHOW_ACTORS_DOES_NOT_EXIST = "Nije moguce dodati glumce za predstavu.";
        public const string CANNOT_CREATE_SHOW_SOME_ACTORS_HAVE_MORE_THAN_TWO_SHOWS_PER_DAY = "Neki glumci imaju 3 ili vise predstava u danu, nije moguce dodati ih za ovu predstavu.";
        public const string SHOW_IN_THE_PAST = "Nije moguce dodati show za datum u proslosti.";

        #endregion

    }
}
