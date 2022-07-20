using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class Dodavanje_Atributa_Uposlenika_I_Datuma_Evidentiranja_Entitetu_Automobil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DatumBrisanja",
                table: "Automobili",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumDodavanja",
                table: "Automobili",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumUredjivanja",
                table: "Automobili",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UposlenikObrisao",
                table: "Automobili",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UposlenikUredjivao",
                table: "Automobili",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatumBrisanja",
                table: "Automobili");

            migrationBuilder.DropColumn(
                name: "DatumDodavanja",
                table: "Automobili");

            migrationBuilder.DropColumn(
                name: "DatumUredjivanja",
                table: "Automobili");

            migrationBuilder.DropColumn(
                name: "UposlenikObrisao",
                table: "Automobili");

            migrationBuilder.DropColumn(
                name: "UposlenikUredjivao",
                table: "Automobili");
        }
    }
}
