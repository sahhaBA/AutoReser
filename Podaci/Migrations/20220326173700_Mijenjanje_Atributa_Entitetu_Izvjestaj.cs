using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class Mijenjanje_Atributa_Entitetu_Izvjestaj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UposlenikDodaoVozilo",
                table: "Izvjestaji");

            migrationBuilder.DropColumn(
                name: "UposlenikObrisao",
                table: "Izvjestaji");

            migrationBuilder.RenameColumn(
                name: "UposlenikUredjivao",
                table: "Izvjestaji",
                newName: "UposlenikKreiraIzvjestaj");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UposlenikKreiraIzvjestaj",
                table: "Izvjestaji",
                newName: "UposlenikUredjivao");

            migrationBuilder.AddColumn<string>(
                name: "UposlenikDodaoVozilo",
                table: "Izvjestaji",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UposlenikObrisao",
                table: "Izvjestaji",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
