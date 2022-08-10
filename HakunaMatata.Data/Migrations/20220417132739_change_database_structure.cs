using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HakunaMatata.Data.Migrations
{
    public partial class change_database_structure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Users_PropertyOwnerUserId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "PropertyOwnerUserId",
                table: "Reservations",
                newName: "PropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_PropertyOwnerUserId",
                table: "Reservations",
                newName: "IX_Reservations_PropertyId");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Reservations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Properties_PropertyId",
                table: "Reservations",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "PropertyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Users_UserId",
                table: "Reservations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Properties_PropertyId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Users_UserId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "PropertyId",
                table: "Reservations",
                newName: "PropertyOwnerUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_PropertyId",
                table: "Reservations",
                newName: "IX_Reservations_PropertyOwnerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Users_PropertyOwnerUserId",
                table: "Reservations",
                column: "PropertyOwnerUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
