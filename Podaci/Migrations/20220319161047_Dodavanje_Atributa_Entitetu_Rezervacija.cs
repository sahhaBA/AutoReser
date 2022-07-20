using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class Dodavanje_Atributa_Entitetu_Rezervacija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminNalogId",
                table: "Rezervacije",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacije_AdminNalogId",
                table: "Rezervacije",
                column: "AdminNalogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacije_KorisnickiNalog_AdminNalogId",
                table: "Rezervacije",
                column: "AdminNalogId",
                principalSchema: "dbo",
                principalTable: "KorisnickiNalog",
                principalColumn: "KorisnickiNalogID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacije_KorisnickiNalog_AdminNalogId",
                table: "Rezervacije");

            migrationBuilder.DropIndex(
                name: "IX_Rezervacije_AdminNalogId",
                table: "Rezervacije");

            migrationBuilder.DropColumn(
                name: "AdminNalogId",
                table: "Rezervacije");
        }
    }
}
