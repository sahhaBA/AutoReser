using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class Dodata_set_osobe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Klijenti_Osoba_OsobaID",
                table: "Klijenti");

            migrationBuilder.DropForeignKey(
                name: "FK_KorisnickiNalog_Osoba_OsobaID",
                table: "KorisnickiNalog");

            migrationBuilder.DropForeignKey(
                name: "FK_Osoba_Gradovi_GradID",
                table: "Osoba");

            migrationBuilder.DropForeignKey(
                name: "FK_Uposlenici_Osoba_OsobaID",
                table: "Uposlenici");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Osoba",
                table: "Osoba");

            migrationBuilder.RenameTable(
                name: "Osoba",
                newName: "Osobe");

            migrationBuilder.RenameIndex(
                name: "IX_Osoba_GradID",
                table: "Osobe",
                newName: "IX_Osobe_GradID");

            migrationBuilder.AddColumn<string>(
                name: "KorisnickoIme",
                table: "Osobe",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lozinka",
                table: "Osobe",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Osobe",
                table: "Osobe",
                column: "OsobaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Klijenti_Osobe_OsobaID",
                table: "Klijenti",
                column: "OsobaID",
                principalTable: "Osobe",
                principalColumn: "OsobaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KorisnickiNalog_Osobe_OsobaID",
                table: "KorisnickiNalog",
                column: "OsobaID",
                principalTable: "Osobe",
                principalColumn: "OsobaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Osobe_Gradovi_GradID",
                table: "Osobe",
                column: "GradID",
                principalTable: "Gradovi",
                principalColumn: "GradID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Uposlenici_Osobe_OsobaID",
                table: "Uposlenici",
                column: "OsobaID",
                principalTable: "Osobe",
                principalColumn: "OsobaID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Klijenti_Osobe_OsobaID",
                table: "Klijenti");

            migrationBuilder.DropForeignKey(
                name: "FK_KorisnickiNalog_Osobe_OsobaID",
                table: "KorisnickiNalog");

            migrationBuilder.DropForeignKey(
                name: "FK_Osobe_Gradovi_GradID",
                table: "Osobe");

            migrationBuilder.DropForeignKey(
                name: "FK_Uposlenici_Osobe_OsobaID",
                table: "Uposlenici");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Osobe",
                table: "Osobe");

            migrationBuilder.DropColumn(
                name: "KorisnickoIme",
                table: "Osobe");

            migrationBuilder.DropColumn(
                name: "Lozinka",
                table: "Osobe");

            migrationBuilder.RenameTable(
                name: "Osobe",
                newName: "Osoba");

            migrationBuilder.RenameIndex(
                name: "IX_Osobe_GradID",
                table: "Osoba",
                newName: "IX_Osoba_GradID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Osoba",
                table: "Osoba",
                column: "OsobaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Klijenti_Osoba_OsobaID",
                table: "Klijenti",
                column: "OsobaID",
                principalTable: "Osoba",
                principalColumn: "OsobaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KorisnickiNalog_Osoba_OsobaID",
                table: "KorisnickiNalog",
                column: "OsobaID",
                principalTable: "Osoba",
                principalColumn: "OsobaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Osoba_Gradovi_GradID",
                table: "Osoba",
                column: "GradID",
                principalTable: "Gradovi",
                principalColumn: "GradID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Uposlenici_Osoba_OsobaID",
                table: "Uposlenici",
                column: "OsobaID",
                principalTable: "Osoba",
                principalColumn: "OsobaID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
