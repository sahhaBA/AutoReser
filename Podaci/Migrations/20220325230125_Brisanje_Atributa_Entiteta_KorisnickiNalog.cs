using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class Brisanje_Atributa_Entiteta_KorisnickiNalog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Admin",
                schema: "dbo",
                table: "KorisnickiNalog");

            migrationBuilder.DropColumn(
                name: "Privilegije",
                schema: "dbo",
                table: "KorisnickiNalog");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Admin",
                schema: "dbo",
                table: "KorisnickiNalog",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Privilegije",
                schema: "dbo",
                table: "KorisnickiNalog",
                type: "bit",
                nullable: true);
        }
    }
}
