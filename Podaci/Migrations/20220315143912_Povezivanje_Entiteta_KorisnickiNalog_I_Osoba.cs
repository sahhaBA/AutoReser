using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class Povezivanje_Entiteta_KorisnickiNalog_I_Osoba : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OsobaID",
                schema: "dbo",
                table: "KorisnickiNalog",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_KorisnickiNalog_OsobaID",
                schema: "dbo",
                table: "KorisnickiNalog",
                column: "OsobaID",
                unique: true,
                filter: "[OsobaID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_KorisnickiNalog_Osobe_OsobaID",
                schema: "dbo",
                table: "KorisnickiNalog",
                column: "OsobaID",
                principalTable: "Osobe",
                principalColumn: "OsobaID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KorisnickiNalog_Osobe_OsobaID",
                schema: "dbo",
                table: "KorisnickiNalog");

            migrationBuilder.DropIndex(
                name: "IX_KorisnickiNalog_OsobaID",
                schema: "dbo",
                table: "KorisnickiNalog");

            migrationBuilder.DropColumn(
                name: "OsobaID",
                schema: "dbo",
                table: "KorisnickiNalog");
        }
    }
}
