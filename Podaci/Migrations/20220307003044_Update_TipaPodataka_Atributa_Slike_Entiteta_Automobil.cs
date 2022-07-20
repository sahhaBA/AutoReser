using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class Update_TipaPodataka_Atributa_Slike_Entiteta_Automobil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slika1",
                table: "Automobili");

            migrationBuilder.DropColumn(
                name: "Slika2",
                table: "Automobili");

            migrationBuilder.DropColumn(
                name: "Slika3",
                table: "Automobili");

            migrationBuilder.DropColumn(
                name: "Slika4",
                table: "Automobili");

            migrationBuilder.DropColumn(
                name: "Slika5",
                table: "Automobili");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Slika1",
                table: "Automobili",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Slika2",
                table: "Automobili",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Slika3",
                table: "Automobili",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Slika4",
                table: "Automobili",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Slika5",
                table: "Automobili",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
