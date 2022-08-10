using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HakunaMatata.Data.Migrations
{
    public partial class foreign_key_properties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Properties_PropertyId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "PropertyId",
                table: "Users",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_PropertyId",
                table: "Users",
                newName: "IX_Users_OwnerId");

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Properties_OwnerId",
                table: "Users",
                column: "OwnerId",
                principalTable: "Properties",
                principalColumn: "PropertyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Properties_OwnerId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Users",
                newName: "PropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_OwnerId",
                table: "Users",
                newName: "IX_Users_PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Properties_PropertyId",
                table: "Users",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "PropertyId");
        }
    }
}
