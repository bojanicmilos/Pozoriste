using System;
using System.Collections.Generic;
using System.Text;

namespace Pozoriste.Domain.Common
{
    public static class Messages
    {
        #region Pieces
        public const string PIECE_DOES_NOT_EXIST = "Komad ne postoji.";
        public const string PIECE_PROPERTIE_TITLE_NOT_VALID = "Naslov komada ne moze biti duzi od 70 karaktera.";
        public const string PIECE_PROPERTIE_YEAR_NOT_VALID = "Godina komada mora biti izmedju 1000-2050.";
        public const string PIECE_CREATION_ERROR = "Greska prilikom kreiranja novog komada, molim vas pokusajte ponovo.";
        #endregion
        #region Actors
        public const string ACTOR_CREATION_ERROR = "Greska prilikom dodavanja novog glumca. Pokusajte ponovo!";
        public const string ACTOR_DOES_NOT_EXIST = "Glumac ne postoji.";
        #endregion
        #region Addresses
        public const string ADDRESS_NOT_FOUND = "Adresa nije pronadjena";
        public const string ADDRESS_PROPERTIE_CITY_NAME_NOT_VALID = "Naziv grada ne moze biti duzi od 50 karaktera.";
        public const string ADDRESS_PROPERTIE_STREET_NAME_NOT_VALID = "Naziv ulice ne može biti duzi od 80 karaktera.";
        public const string ADDRESS_CREATION_ERROR = "Greska prilikom kreiranja nove adrese, molim vas pokusajte ponovo.";



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
        #region Theatres
        public const string THEATRE_GET_ALL_THEATRES_ERROR = "Greska prilikom dobijanja svih pozorista, molim vas pokusajte ponovo.";
        public const string THEATRE_NAME_NOT_VALID = "Pozoriste ne moze imati vise od 50 znakova.";
        public const string THEATRE_DOES_NOT_EXIST = "Pozoriste ne postoji.";
        public const string THEATRE_CREATION_ERROR = "Greska prilikom kreiranja novog pozorista, molim vas pokusajte ponovo.";
        #endregion
        #region Auditoriums
        public const string AUDITORIUM_PROPERTY_NAME_NOT_VALID = "Ime sale ne moze biti duze od 50 znakova.";
        public const string AUDITORIUM_PROPERTY_SEATROWSNUMBER_NOT_VALID = "Broj sedista u sali mora biti između 1-20.";
        public const string AUDITORIUM_PROPERTY_SEATNUMBER_NOT_VALID = "Broj mesta u sali mora biti između 1-20.";
        #endregion

    }
}
