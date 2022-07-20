using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class Uklanjanje_Vanjskog_Kljuca_Automobil_Kod_Entiteta_Izvjestaj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Izvjestaji_Automobili_AutomobilID",
                table: "Izvjestaji");

            migrationBuilder.DropIndex(
                name: "IX_Izvjestaji_AutomobilID",
                table: "Izvjestaji");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Izvjestaji_AutomobilID",
                table: "Izvjestaji",
                column: "AutomobilID");

            migrationBuilder.AddForeignKey(
                name: "FK_Izvjestaji_Automobili_AutomobilID",
                table: "Izvjestaji",
                column: "AutomobilID",
                principalTable: "Automobili",
                principalColumn: "AutomobilID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
