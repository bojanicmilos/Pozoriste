using System;
using System.Collections.Generic;
using System.Text;

namespace Pozoriste.Domain.Common
{
    public static class Messages
    {
        #region Pieces
        public const string PIECE_DOES_NOT_EXIST = "Komad ne postoji.";
        public const string PIECE_PROPERTIE_TITLE_NOT_VALID = "Naslov komada ne može biti duži od 70 karaktera.";
        public const string PIECE_PROPERTIE_YEAR_NOT_VALID = "Godina komada mora biti između 1000-2050.";
        public const string PIECE_CREATION_ERROR = "Greška prilikom kreiranja novog komada, molim vas pokušajte ponovo.";
        #endregion
        #region Actors
        public const string ACTOR_CREATION_ERROR = "Greška prilikom dodavanja novog glumca. Pokušajte ponovo!";
        public const string ACTOR_DOES_NOT_EXIST = "Glumac ne postoji.";
        #endregion
        #region Addresses
        public const string ADDRESS_NOT_FOUND = "Adresa nije pronadjena";
        public const string ADDRESS_PROPERTIE_CITY_NAME_NOT_VALID = "Naziv grada ne može biti duži od 50 karaktera.";
        public const string ADDRESS_PROPERTIE_STREET_NAME_NOT_VALID = "Naziv ulice ne može biti duži od 80 karaktera.";
        public const string ADDRESS_CREATION_ERROR = "Greška prilikom kreiranja nove adrese, molim vas pokušajte ponovo.";



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
