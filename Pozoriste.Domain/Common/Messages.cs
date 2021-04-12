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
        public const string SHOW_DOES_NOT_EXIST_FOR_RESERVATION = "Neophodno je odabrati odgovarajucu predstavu za rezervaciju.";
        public const string USER_FOR_RESERVATION_DOES_NOT_EXIST = "Please login to make reservation.";
        public const string SHOW_FOR_THAT_PIECE_ARE_STILL_ONGOING = "Predstave za komad jos uvek traju.";
        public const string CANNOT_DELETE_SHOW = "Nije moguce obrisati predstavu";

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
        public const string AUDITORIUM_INVALID_CINEMAID = "Ne moze se kreirati nova sala, sala sa prosledjenim cinemaId-jem ne postoji.";
        public const string AUDITORIUM_CREATION_ERROR = "Doslo je do greske prilikom kreiranja nove sale, pokusajte ponovo.";
        public const string AUDITORIUM_NOT_FOUND = "Sala ne postoji.";
        public const string AUDITORIUM_HAS_FUTURE_SHOWS = "Sala ima buduce predstave.";
        #endregion
        #region Seats
        public const string SEAT_DOES_NOT_EXIST_FOR_AUDITORIUM = "Zahtevana sedista za rezervaciju ne postoje u auditoriujumu";
        public const string SEATS_CANNOT_BE_DUPLICATES = "Zahtevana sedista su duplikati.";
        public const string SEATS_NOT_IN_THE_SAME_ROW = "Sedista moraju biti u istom redu.";
        public const string SEATS_MUST_BE_NEXT_TO_EACH_OTHER = "Rezervisana sedista moraju biti jedno pored drugog";
        public const string SEATS_ALREADY_TAKEN_ERROR = "Sedista su vec zauzeta";
        public const string SEAT_AUDITORIUM_NOT_FOUND = "Nisu pronadjena sedista za auditorium.";
        #endregion
        #region Users
        public const string USER_NOT_FOUND = "Korisnik nije pronadjen.";
        #endregion
    }
}
