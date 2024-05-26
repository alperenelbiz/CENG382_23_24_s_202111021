using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Challenges_AspNetUsers_CreatedById",
                table: "Challenges");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Comments",
                newName: "CreatedTime");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Challenges",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Challenges",
                newName: "CreatTime");

            migrationBuilder.RenameIndex(
                name: "IX_Challenges_CreatedById",
                table: "Challenges",
                newName: "IX_Challenges_CreatorId");

            migrationBuilder.RenameColumn(
                name: "ProfilePictureUrl",
                table: "AspNetUsers",
                newName: "ProfilePicture");

            migrationBuilder.AddForeignKey(
                name: "FK_Challenges_AspNetUsers_CreatorId",
                table: "Challenges",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Challenges_AspNetUsers_CreatorId",
                table: "Challenges");

            migrationBuilder.RenameColumn(
                name: "CreatedTime",
                table: "Comments",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Challenges",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "CreatTime",
                table: "Challenges",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_Challenges_CreatorId",
                table: "Challenges",
                newName: "IX_Challenges_CreatedById");

            migrationBuilder.RenameColumn(
                name: "ProfilePicture",
                table: "AspNetUsers",
                newName: "ProfilePictureUrl");

            migrationBuilder.AddForeignKey(
                name: "FK_Challenges_AspNetUsers_CreatedById",
                table: "Challenges",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
