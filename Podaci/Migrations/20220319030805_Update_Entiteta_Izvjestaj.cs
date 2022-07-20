using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class Update_Entiteta_Izvjestaj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Izvjestaji_Uposlenici_UposlenikID",
                table: "Izvjestaji");

            migrationBuilder.DropIndex(
                name: "IX_Izvjestaji_UposlenikID",
                table: "Izvjestaji");

            migrationBuilder.DropColumn(
                name: "UposlenikID",
                table: "Izvjestaji");

            migrationBuilder.AddColumn<string>(
                name: "KorisnickiNalogID",
                table: "Izvjestaji",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UposlenikDodaoVozilo",
                table: "Izvjestaji",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Izvjestaji_KorisnickiNalogID",
                table: "Izvjestaji",
                column: "KorisnickiNalogID");

            migrationBuilder.AddForeignKey(
                name: "FK_Izvjestaji_KorisnickiNalog_KorisnickiNalogID",
                table: "Izvjestaji",
                column: "KorisnickiNalogID",
                principalSchema: "dbo",
                principalTable: "KorisnickiNalog",
                principalColumn: "KorisnickiNalogID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Izvjestaji_KorisnickiNalog_KorisnickiNalogID",
                table: "Izvjestaji");

            migrationBuilder.DropIndex(
                name: "IX_Izvjestaji_KorisnickiNalogID",
                table: "Izvjestaji");

            migrationBuilder.DropColumn(
                name: "KorisnickiNalogID",
                table: "Izvjestaji");

            migrationBuilder.DropColumn(
                name: "UposlenikDodaoVozilo",
                table: "Izvjestaji");

            migrationBuilder.AddColumn<int>(
                name: "UposlenikID",
                table: "Izvjestaji",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Izvjestaji_UposlenikID",
                table: "Izvjestaji",
                column: "UposlenikID");

            migrationBuilder.AddForeignKey(
                name: "FK_Izvjestaji_Uposlenici_UposlenikID",
                table: "Izvjestaji",
                column: "UposlenikID",
                principalTable: "Uposlenici",
                principalColumn: "UposlenikID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
