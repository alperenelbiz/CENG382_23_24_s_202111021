using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lab11.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddReservations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Room",
                table: "Room");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation");

            migrationBuilder.RenameTable(
                name: "Room",
                newName: "Rooms");

            migrationBuilder.RenameTable(
                name: "Reservation",
                newName: "Reservations");

            migrationBuilder.RenameColumn(
                name: "ReservationDate",
                table: "Reservations",
                newName: "ReservationDateTime");

            migrationBuilder.RenameColumn(
                name: "CustomerName",
                table: "Reservations",
                newName: "ReservedBy");

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RoomId",
                table: "Reservations",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Rooms_RoomId",
                table: "Reservations",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Rooms_RoomId",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_RoomId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Reservations");

            migrationBuilder.RenameTable(
                name: "Rooms",
                newName: "Room");

            migrationBuilder.RenameTable(
                name: "Reservations",
                newName: "Reservation");

            migrationBuilder.RenameColumn(
                name: "ReservedBy",
                table: "Reservation",
                newName: "CustomerName");

            migrationBuilder.RenameColumn(
                name: "ReservationDateTime",
                table: "Reservation",
                newName: "ReservationDate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Room",
                table: "Room",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation",
                column: "Id");
        }
    }
}
