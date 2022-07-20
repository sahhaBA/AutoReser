using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class Dodavanje_Atributa_Uposlenik_Entitetu_Automobil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UposlenikID",
                table: "Automobili",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Automobili_UposlenikID",
                table: "Automobili",
                column: "UposlenikID");

            migrationBuilder.AddForeignKey(
                name: "FK_Automobili_Uposlenici_UposlenikID",
                table: "Automobili",
                column: "UposlenikID",
                principalTable: "Uposlenici",
                principalColumn: "UposlenikID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Automobili_Uposlenici_UposlenikID",
                table: "Automobili");

            migrationBuilder.DropIndex(
                name: "IX_Automobili_UposlenikID",
                table: "Automobili");

            migrationBuilder.DropColumn(
                name: "UposlenikID",
                table: "Automobili");
        }
    }
}
