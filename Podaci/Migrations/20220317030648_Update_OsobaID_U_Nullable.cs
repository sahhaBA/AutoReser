using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class Update_OsobaID_U_Nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Klijenti_Osobe_OsobaID",
                table: "Klijenti");

            migrationBuilder.DropForeignKey(
                name: "FK_KorisnickiNalog_Osobe_OsobaID",
                schema: "dbo",
                table: "KorisnickiNalog");

            migrationBuilder.DropIndex(
                name: "IX_Klijenti_OsobaID",
                table: "Klijenti");

            migrationBuilder.AlterColumn<string>(
                name: "Prezime",
                table: "Osobe",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ime",
                table: "Osobe",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OsobaID",
                table: "Klijenti",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Klijenti_OsobaID",
                table: "Klijenti",
                column: "OsobaID",
                unique: true,
                filter: "[OsobaID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Klijenti_Osobe_OsobaID",
                table: "Klijenti",
                column: "OsobaID",
                principalTable: "Osobe",
                principalColumn: "OsobaID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KorisnickiNalog_Osobe_OsobaID",
                schema: "dbo",
                table: "KorisnickiNalog",
                column: "OsobaID",
                principalTable: "Osobe",
                principalColumn: "OsobaID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Klijenti_Osobe_OsobaID",
                table: "Klijenti");

            migrationBuilder.DropForeignKey(
                name: "FK_KorisnickiNalog_Osobe_OsobaID",
                schema: "dbo",
                table: "KorisnickiNalog");

            migrationBuilder.DropIndex(
                name: "IX_Klijenti_OsobaID",
                table: "Klijenti");

            migrationBuilder.AlterColumn<string>(
                name: "Prezime",
                table: "Osobe",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Ime",
                table: "Osobe",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "OsobaID",
                table: "Klijenti",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Klijenti_OsobaID",
                table: "Klijenti",
                column: "OsobaID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Klijenti_Osobe_OsobaID",
                table: "Klijenti",
                column: "OsobaID",
                principalTable: "Osobe",
                principalColumn: "OsobaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KorisnickiNalog_Osobe_OsobaID",
                schema: "dbo",
                table: "KorisnickiNalog",
                column: "OsobaID",
                principalTable: "Osobe",
                principalColumn: "OsobaID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
