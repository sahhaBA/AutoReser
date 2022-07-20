using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class Dodavanje_Atributa_Entitetu_Automobil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Slika",
                table: "Automobili",
                newName: "Slika1");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "Slika5",
                table: "Automobili",
                newName: "Slika");
        }
    }
}
