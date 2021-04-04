﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pozoriste.Data.Context;

namespace Pozoriste.Data.Migrations
{
    [DbContext(typeof(TheatreContext))]
    [Migration("20210404131654_fixTableNames")]
    partial class fixTableNames
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Pozoriste.Data.Entities.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("actor");
                });

            modelBuilder.Entity("Pozoriste.Data.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CityName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("address");
                });

            modelBuilder.Entity("Pozoriste.Data.Entities.Auditorium", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TheatreId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TheatreId");

                    b.ToTable("auditorium");
                });

            modelBuilder.Entity("Pozoriste.Data.Entities.Piece", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Genre")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("piece");
                });

            modelBuilder.Entity("Pozoriste.Data.Entities.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ShowId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ShowId");

                    b.HasIndex("UserId");

                    b.ToTable("reservation");
                });

            modelBuilder.Entity("Pozoriste.Data.Entities.ReservationSeat", b =>
                {
                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.Property<int>("SeatId")
                        .HasColumnType("int");

                    b.HasKey("ReservationId", "SeatId");

                    b.HasIndex("SeatId");

                    b.ToTable("ReservationSeat");
                });

            modelBuilder.Entity("Pozoriste.Data.Entities.Seat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuditoriumId")
                        .HasColumnType("int");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("Row")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuditoriumId");

                    b.ToTable("seat");
                });

            modelBuilder.Entity("Pozoriste.Data.Entities.Show", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuditoriumId")
                        .HasColumnType("int");

                    b.Property<int>("PieceId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ShowTime")
                        .HasColumnType("datetime2");

                    b.Property<double>("TicketPrice")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("AuditoriumId");

                    b.HasIndex("PieceId");

                    b.ToTable("show");
                });

            modelBuilder.Entity("Pozoriste.Data.Entities.ShowActor", b =>
                {
                    b.Property<int>("ShowId")
                        .HasColumnType("int");

                    b.Property<int>("ActorId")
                        .HasColumnType("int");

                    b.HasKey("ShowId", "ActorId");

                    b.HasIndex("ActorId");

                    b.ToTable("ShowActor");
                });

            modelBuilder.Entity("Pozoriste.Data.Entities.Theatre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.ToTable("theatre");
                });

            modelBuilder.Entity("Pozoriste.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("Pozoriste.Data.Entities.Auditorium", b =>
                {
                    b.HasOne("Pozoriste.Data.Entities.Theatre", "Theatre")
                        .WithMany("Auditoriums")
                        .HasForeignKey("TheatreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Theatre");
                });

            modelBuilder.Entity("Pozoriste.Data.Entities.Reservation", b =>
                {
                    b.HasOne("Pozoriste.Data.Entities.Show", "Show")
                        .WithMany("Reservations")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pozoriste.Data.Entities.User", "User")
                        .WithMany("Reservations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Show");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Pozoriste.Data.Entities.ReservationSeat", b =>
                {
                    b.HasOne("Pozoriste.Data.Entities.Reservation", "Reservation")
                        .WithMany("ReservationSeats")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pozoriste.Data.Entities.Seat", "Seat")
                        .WithMany("ReservationSeats")
                        .HasForeignKey("SeatId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Reservation");

                    b.Navigation("Seat");
                });

            modelBuilder.Entity("Pozoriste.Data.Entities.Seat", b =>
                {
                    b.HasOne("Pozoriste.Data.Entities.Auditorium", "Auditorium")
                        .WithMany("Seats")
                        .HasForeignKey("AuditoriumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Auditorium");
                });

            modelBuilder.Entity("Pozoriste.Data.Entities.Show", b =>
                {
                    b.HasOne("Pozoriste.Data.Entities.Auditorium", "Auditorium")
                        .WithMany("Shows")
                        .HasForeignKey("AuditoriumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pozoriste.Data.Entities.Piece", "Piece")
                        .WithMany("Shows")
                        .HasForeignKey("PieceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Auditorium");

                    b.Navigation("Piece");
                });

            modelBuilder.Entity("Pozoriste.Data.Entities.ShowActor", b =>
                {
                    b.HasOne("Pozoriste.Data.Entities.Actor", "Actor")
                        .WithMany("ShowActors")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pozoriste.Data.Entities.Show", "Show")
                        .WithMany("ShowActors")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("Show");
                });

            modelBuilder.Entity("Pozoriste.Data.Entities.Theatre", b =>
                {
                    b.HasOne("Pozoriste.Data.Entities.Address", "Address")
                        .WithOne("Theatre")
                        .HasForeignKey("Pozoriste.Data.Entities.Theatre", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Pozoriste.Data.Entities.Actor", b =>
                {
                    b.Navigation("ShowActors");
                });

            modelBuilder.Entity("Pozoriste.Data.Entities.Address", b =>
                {
                    b.Navigation("Theatre");
                });

            modelBuilder.Entity("Pozoriste.Data.Entities.Auditorium", b =>
                {
                    b.Navigation("Seats");

                    b.Navigation("Shows");
                });

            modelBuilder.Entity("Pozoriste.Data.Entities.Piece", b =>
                {
                    b.Navigation("Shows");
                });

            modelBuilder.Entity("Pozoriste.Data.Entities.Reservation", b =>
                {
                    b.Navigation("ReservationSeats");
                });

            modelBuilder.Entity("Pozoriste.Data.Entities.Seat", b =>
                {
                    b.Navigation("ReservationSeats");
                });

            modelBuilder.Entity("Pozoriste.Data.Entities.Show", b =>
                {
                    b.Navigation("Reservations");

                    b.Navigation("ShowActors");
                });

            modelBuilder.Entity("Pozoriste.Data.Entities.Theatre", b =>
                {
                    b.Navigation("Auditoriums");
                });

            modelBuilder.Entity("Pozoriste.Data.Entities.User", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
