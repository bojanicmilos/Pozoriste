using Microsoft.EntityFrameworkCore.Migrations;

namespace Pozoriste.Data.Migrations
{
    public partial class fixTableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auditoriums_Theatres_TheatreId",
                table: "Auditoriums");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Shows_ShowId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Users_UserId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationSeat_Reservations_ReservationId",
                table: "ReservationSeat");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationSeat_Seats_SeatId",
                table: "ReservationSeat");

            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Auditoriums_AuditoriumId",
                table: "Seats");

            migrationBuilder.DropForeignKey(
                name: "FK_ShowActor_Actors_ActorId",
                table: "ShowActor");

            migrationBuilder.DropForeignKey(
                name: "FK_ShowActor_Shows_ShowId",
                table: "ShowActor");

            migrationBuilder.DropForeignKey(
                name: "FK_Shows_Auditoriums_AuditoriumId",
                table: "Shows");

            migrationBuilder.DropForeignKey(
                name: "FK_Shows_Pieces_PieceId",
                table: "Shows");

            migrationBuilder.DropForeignKey(
                name: "FK_Theatres_Addresses_AddressId",
                table: "Theatres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Theatres",
                table: "Theatres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shows",
                table: "Shows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seats",
                table: "Seats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pieces",
                table: "Pieces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Auditoriums",
                table: "Auditoriums");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Actors",
                table: "Actors");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "user");

            migrationBuilder.RenameTable(
                name: "Theatres",
                newName: "theatre");

            migrationBuilder.RenameTable(
                name: "Shows",
                newName: "show");

            migrationBuilder.RenameTable(
                name: "Seats",
                newName: "seat");

            migrationBuilder.RenameTable(
                name: "Reservations",
                newName: "reservation");

            migrationBuilder.RenameTable(
                name: "Pieces",
                newName: "piece");

            migrationBuilder.RenameTable(
                name: "Auditoriums",
                newName: "auditorium");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "address");

            migrationBuilder.RenameTable(
                name: "Actors",
                newName: "actor");

            migrationBuilder.RenameIndex(
                name: "IX_Theatres_AddressId",
                table: "theatre",
                newName: "IX_theatre_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Shows_PieceId",
                table: "show",
                newName: "IX_show_PieceId");

            migrationBuilder.RenameIndex(
                name: "IX_Shows_AuditoriumId",
                table: "show",
                newName: "IX_show_AuditoriumId");

            migrationBuilder.RenameIndex(
                name: "IX_Seats_AuditoriumId",
                table: "seat",
                newName: "IX_seat_AuditoriumId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_UserId",
                table: "reservation",
                newName: "IX_reservation_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_ShowId",
                table: "reservation",
                newName: "IX_reservation_ShowId");

            migrationBuilder.RenameIndex(
                name: "IX_Auditoriums_TheatreId",
                table: "auditorium",
                newName: "IX_auditorium_TheatreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                table: "user",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_theatre",
                table: "theatre",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_show",
                table: "show",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_seat",
                table: "seat",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_reservation",
                table: "reservation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_piece",
                table: "piece",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_auditorium",
                table: "auditorium",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_address",
                table: "address",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_actor",
                table: "actor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_auditorium_theatre_TheatreId",
                table: "auditorium",
                column: "TheatreId",
                principalTable: "theatre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reservation_show_ShowId",
                table: "reservation",
                column: "ShowId",
                principalTable: "show",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reservation_user_UserId",
                table: "reservation",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationSeat_reservation_ReservationId",
                table: "ReservationSeat",
                column: "ReservationId",
                principalTable: "reservation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationSeat_seat_SeatId",
                table: "ReservationSeat",
                column: "SeatId",
                principalTable: "seat",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_seat_auditorium_AuditoriumId",
                table: "seat",
                column: "AuditoriumId",
                principalTable: "auditorium",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_show_auditorium_AuditoriumId",
                table: "show",
                column: "AuditoriumId",
                principalTable: "auditorium",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_show_piece_PieceId",
                table: "show",
                column: "PieceId",
                principalTable: "piece",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShowActor_actor_ActorId",
                table: "ShowActor",
                column: "ActorId",
                principalTable: "actor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShowActor_show_ShowId",
                table: "ShowActor",
                column: "ShowId",
                principalTable: "show",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_theatre_address_AddressId",
                table: "theatre",
                column: "AddressId",
                principalTable: "address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_auditorium_theatre_TheatreId",
                table: "auditorium");

            migrationBuilder.DropForeignKey(
                name: "FK_reservation_show_ShowId",
                table: "reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_reservation_user_UserId",
                table: "reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationSeat_reservation_ReservationId",
                table: "ReservationSeat");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationSeat_seat_SeatId",
                table: "ReservationSeat");

            migrationBuilder.DropForeignKey(
                name: "FK_seat_auditorium_AuditoriumId",
                table: "seat");

            migrationBuilder.DropForeignKey(
                name: "FK_show_auditorium_AuditoriumId",
                table: "show");

            migrationBuilder.DropForeignKey(
                name: "FK_show_piece_PieceId",
                table: "show");

            migrationBuilder.DropForeignKey(
                name: "FK_ShowActor_actor_ActorId",
                table: "ShowActor");

            migrationBuilder.DropForeignKey(
                name: "FK_ShowActor_show_ShowId",
                table: "ShowActor");

            migrationBuilder.DropForeignKey(
                name: "FK_theatre_address_AddressId",
                table: "theatre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                table: "user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_theatre",
                table: "theatre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_show",
                table: "show");

            migrationBuilder.DropPrimaryKey(
                name: "PK_seat",
                table: "seat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_reservation",
                table: "reservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_piece",
                table: "piece");

            migrationBuilder.DropPrimaryKey(
                name: "PK_auditorium",
                table: "auditorium");

            migrationBuilder.DropPrimaryKey(
                name: "PK_address",
                table: "address");

            migrationBuilder.DropPrimaryKey(
                name: "PK_actor",
                table: "actor");

            migrationBuilder.RenameTable(
                name: "user",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "theatre",
                newName: "Theatres");

            migrationBuilder.RenameTable(
                name: "show",
                newName: "Shows");

            migrationBuilder.RenameTable(
                name: "seat",
                newName: "Seats");

            migrationBuilder.RenameTable(
                name: "reservation",
                newName: "Reservations");

            migrationBuilder.RenameTable(
                name: "piece",
                newName: "Pieces");

            migrationBuilder.RenameTable(
                name: "auditorium",
                newName: "Auditoriums");

            migrationBuilder.RenameTable(
                name: "address",
                newName: "Addresses");

            migrationBuilder.RenameTable(
                name: "actor",
                newName: "Actors");

            migrationBuilder.RenameIndex(
                name: "IX_theatre_AddressId",
                table: "Theatres",
                newName: "IX_Theatres_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_show_PieceId",
                table: "Shows",
                newName: "IX_Shows_PieceId");

            migrationBuilder.RenameIndex(
                name: "IX_show_AuditoriumId",
                table: "Shows",
                newName: "IX_Shows_AuditoriumId");

            migrationBuilder.RenameIndex(
                name: "IX_seat_AuditoriumId",
                table: "Seats",
                newName: "IX_Seats_AuditoriumId");

            migrationBuilder.RenameIndex(
                name: "IX_reservation_UserId",
                table: "Reservations",
                newName: "IX_Reservations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_reservation_ShowId",
                table: "Reservations",
                newName: "IX_Reservations_ShowId");

            migrationBuilder.RenameIndex(
                name: "IX_auditorium_TheatreId",
                table: "Auditoriums",
                newName: "IX_Auditoriums_TheatreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Theatres",
                table: "Theatres",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shows",
                table: "Shows",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seats",
                table: "Seats",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pieces",
                table: "Pieces",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Auditoriums",
                table: "Auditoriums",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Actors",
                table: "Actors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Auditoriums_Theatres_TheatreId",
                table: "Auditoriums",
                column: "TheatreId",
                principalTable: "Theatres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Shows_ShowId",
                table: "Reservations",
                column: "ShowId",
                principalTable: "Shows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Users_UserId",
                table: "Reservations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationSeat_Reservations_ReservationId",
                table: "ReservationSeat",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationSeat_Seats_SeatId",
                table: "ReservationSeat",
                column: "SeatId",
                principalTable: "Seats",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Auditoriums_AuditoriumId",
                table: "Seats",
                column: "AuditoriumId",
                principalTable: "Auditoriums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShowActor_Actors_ActorId",
                table: "ShowActor",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShowActor_Shows_ShowId",
                table: "ShowActor",
                column: "ShowId",
                principalTable: "Shows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shows_Auditoriums_AuditoriumId",
                table: "Shows",
                column: "AuditoriumId",
                principalTable: "Auditoriums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shows_Pieces_PieceId",
                table: "Shows",
                column: "PieceId",
                principalTable: "Pieces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Theatres_Addresses_AddressId",
                table: "Theatres",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
