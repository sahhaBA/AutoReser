using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class Brisanje_Atributa_Spol_Entitetu_Uposlenik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uposlenici_Spol_SpolID",
                table: "Uposlenici");

            migrationBuilder.DropIndex(
                name: "IX_Uposlenici_SpolID",
                table: "Uposlenici");

            migrationBuilder.DropColumn(
                name: "SpolID",
                table: "Uposlenici");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpolID",
                table: "Uposlenici",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Uposlenici_SpolID",
                table: "Uposlenici",
                column: "SpolID");

            migrationBuilder.AddForeignKey(
                name: "FK_Uposlenici_Spol_SpolID",
                table: "Uposlenici",
                column: "SpolID",
                principalTable: "Spol",
                principalColumn: "SpolID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
