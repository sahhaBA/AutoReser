using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class Dodavanje_Atributa_Email_Entitetu_KorisnickiNalog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "KorisnickiNalog",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "KorisnickiNalog");
        }
    }
}
