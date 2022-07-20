using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class Nullabe_Spol_I_Grad_Entiteta_Osoba : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Osobe_Gradovi_GradID",
                table: "Osobe");

            migrationBuilder.DropForeignKey(
                name: "FK_Osobe_Spol_SpolID",
                table: "Osobe");

            migrationBuilder.AlterColumn<int>(
                name: "SpolID",
                table: "Osobe",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GradID",
                table: "Osobe",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Osobe_Gradovi_GradID",
                table: "Osobe",
                column: "GradID",
                principalTable: "Gradovi",
                principalColumn: "GradID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Osobe_Spol_SpolID",
                table: "Osobe",
                column: "SpolID",
                principalTable: "Spol",
                principalColumn: "SpolID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Osobe_Gradovi_GradID",
                table: "Osobe");

            migrationBuilder.DropForeignKey(
                name: "FK_Osobe_Spol_SpolID",
                table: "Osobe");

            migrationBuilder.AlterColumn<int>(
                name: "SpolID",
                table: "Osobe",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GradID",
                table: "Osobe",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Osobe_Gradovi_GradID",
                table: "Osobe",
                column: "GradID",
                principalTable: "Gradovi",
                principalColumn: "GradID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Osobe_Spol_SpolID",
                table: "Osobe",
                column: "SpolID",
                principalTable: "Spol",
                principalColumn: "SpolID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
