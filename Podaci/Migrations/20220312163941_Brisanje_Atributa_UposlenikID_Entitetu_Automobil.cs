using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class Brisanje_Atributa_UposlenikID_Entitetu_Automobil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UposlenikID",
                table: "Automobili");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UposlenikID",
                table: "Automobili",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
