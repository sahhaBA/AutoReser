using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class Dodavanje_Atributa_Entitetu_Izvjestaj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AutomobilID",
                table: "Izvjestaji",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumBrisanja",
                table: "Izvjestaji",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumDodavanja",
                table: "Izvjestaji",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumUredjivanja",
                table: "Izvjestaji",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UposlenikObrisao",
                table: "Izvjestaji",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UposlenikUredjivao",
                table: "Izvjestaji",
                type: "nvarchar(max)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Izvjestaji_Automobili_AutomobilID",
                table: "Izvjestaji");

            migrationBuilder.DropIndex(
                name: "IX_Izvjestaji_AutomobilID",
                table: "Izvjestaji");

            migrationBuilder.DropColumn(
                name: "AutomobilID",
                table: "Izvjestaji");

            migrationBuilder.DropColumn(
                name: "DatumBrisanja",
                table: "Izvjestaji");

            migrationBuilder.DropColumn(
                name: "DatumDodavanja",
                table: "Izvjestaji");

            migrationBuilder.DropColumn(
                name: "DatumUredjivanja",
                table: "Izvjestaji");

            migrationBuilder.DropColumn(
                name: "UposlenikObrisao",
                table: "Izvjestaji");

            migrationBuilder.DropColumn(
                name: "UposlenikUredjivao",
                table: "Izvjestaji");
        }
    }
}
