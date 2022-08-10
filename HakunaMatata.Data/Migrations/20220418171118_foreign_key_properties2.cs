using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HakunaMatata.Data.Migrations
{
    public partial class foreign_key_properties2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Properties_OwnerId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_OwnerId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_OwnerId",
                table: "Users",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Properties_OwnerId",
                table: "Users",
                column: "OwnerId",
                principalTable: "Properties",
                principalColumn: "PropertyId");
        }
    }
}
