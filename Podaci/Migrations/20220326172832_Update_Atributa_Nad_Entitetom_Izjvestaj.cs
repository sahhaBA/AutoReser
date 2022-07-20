using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class Update_Atributa_Nad_Entitetom_Izjvestaj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatumBrisanja",
                table: "Izvjestaji");

            migrationBuilder.DropColumn(
                name: "DatumDodavanja",
                table: "Izvjestaji");

            migrationBuilder.RenameColumn(
                name: "DatumUredjivanja",
                table: "Izvjestaji",
                newName: "DatumIzvjestaja");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DatumIzvjestaja",
                table: "Izvjestaji",
                newName: "DatumUredjivanja");

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumBrisanja",
                table: "Izvjestaji",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumDodavanja",
                table: "Izvjestaji",
                type: "datetime2",
                nullable: true);
        }
    }
}
