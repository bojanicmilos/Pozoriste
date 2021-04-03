using Microsoft.EntityFrameworkCore;
using Pozoriste.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pozoriste.Data.Context
{
    public class TheatreContext : DbContext
    {
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Auditorium> Auditoriums { get; set; }
        public DbSet<Piece> Pieces { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<Theatre> Theatres { get; set; }
        public DbSet<User> Users { get; set; }

        public TheatreContext(DbContextOptions options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Address>()
                .HasOne(x => x.Theatre)
                .WithOne(x => x.Address)
                .HasForeignKey<Theatre>(x => x.AddressId)
                .IsRequired();

            modelBuilder.Entity<Theatre>()
                .HasMany(x => x.Auditoriums)
                .WithOne(x => x.Theatre)
                .HasForeignKey(x => x.TheatreId)
                .IsRequired();

            modelBuilder.Entity<Auditorium>()
                .HasMany(x => x.Seats)
                .WithOne(x => x.Auditorium)
                .HasForeignKey(x => x.AuditoriumId)
                .IsRequired();

            modelBuilder.Entity<Auditorium>()
                .HasMany(x => x.Shows)
                .WithOne(x => x.Auditorium)
                .HasForeignKey(x => x.AuditoriumId)
                .IsRequired();
                

            modelBuilder.Entity<Show>()
                .HasOne(x => x.Piece)
                .WithMany(x => x.Shows)
                .HasForeignKey(x => x.PieceId)
                .IsRequired();

            modelBuilder.Entity<Show>()
                .HasMany(x => x.Reservations)
                .WithOne(x => x.Show)
                .HasForeignKey(x => x.ShowId)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasMany(x => x.Reservations)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            modelBuilder.Entity<ShowActor>()
                .HasKey(showactor => new
                {
                    showactor.ShowId,
                    showactor.ActorId
                });

            modelBuilder.Entity<ShowActor>()
                .HasOne(x => x.Actor)
                .WithMany(x => x.ShowActors)
                .HasForeignKey(x => x.ActorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ShowActor>()
                .HasOne(x => x.Show)
                .WithMany(x => x.ShowActors)
                .HasForeignKey(x => x.ShowId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<ReservationSeat>()
                .HasKey(reservationSeat => new
                {
                    reservationSeat.ReservationId,
                    reservationSeat.SeatId
                });


            modelBuilder.Entity<ReservationSeat>()
                .HasOne(x => x.Reservation)
                .WithMany(x => x.ReservationSeats)
                .HasForeignKey(x => x.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReservationSeat>()
                .HasOne(x => x.Seat)
                .WithMany(x => x.ReservationSeats)
                .HasForeignKey(x => x.SeatId)
                .OnDelete(DeleteBehavior.NoAction);

        }

    }


}
