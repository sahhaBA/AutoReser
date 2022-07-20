using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class Dodavanje_Atributa_Spol_Entitetu_Osoba : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpolID",
                table: "Osobe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Osobe_SpolID",
                table: "Osobe",
                column: "SpolID");

            migrationBuilder.AddForeignKey(
                name: "FK_Osobe_Spol_SpolID",
                table: "Osobe",
                column: "SpolID",
                principalTable: "Spol",
                principalColumn: "SpolID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Osobe_Spol_SpolID",
                table: "Osobe");

            migrationBuilder.DropIndex(
                name: "IX_Osobe_SpolID",
                table: "Osobe");

            migrationBuilder.DropColumn(
                name: "SpolID",
                table: "Osobe");
        }
    }
}
