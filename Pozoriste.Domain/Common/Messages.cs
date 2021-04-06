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

    }
}
