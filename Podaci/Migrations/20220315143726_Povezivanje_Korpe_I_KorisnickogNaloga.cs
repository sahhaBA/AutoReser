using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class Povezivanje_Korpe_I_KorisnickogNaloga : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KorpaID",
                table: "Korpa",
                newName: "AutomobilID");

            migrationBuilder.AddColumn<string>(
                name: "KorisnickiNalogID",
                table: "Korpa",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Korpa",
                table: "Korpa",
                columns: new[] { "KorisnickiNalogID", "AutomobilID" });

            migrationBuilder.CreateIndex(
                name: "IX_Korpa_AutomobilID",
                table: "Korpa",
                column: "AutomobilID");

            migrationBuilder.AddForeignKey(
                name: "FK_Korpa_Automobili_AutomobilID",
                table: "Korpa",
                column: "AutomobilID",
                principalTable: "Automobili",
                principalColumn: "AutomobilID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Korpa_KorisnickiNalog_KorisnickiNalogID",
                table: "Korpa",
                column: "KorisnickiNalogID",
                principalSchema: "dbo",
                principalTable: "KorisnickiNalog",
                principalColumn: "KorisnickiNalogID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Korpa_Automobili_AutomobilID",
                table: "Korpa");

            migrationBuilder.DropForeignKey(
                name: "FK_Korpa_KorisnickiNalog_KorisnickiNalogID",
                table: "Korpa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Korpa",
                table: "Korpa");

            migrationBuilder.DropIndex(
                name: "IX_Korpa_AutomobilID",
                table: "Korpa");

            migrationBuilder.DropColumn(
                name: "KorisnickiNalogID",
                table: "Korpa");

            migrationBuilder.RenameColumn(
                name: "AutomobilID",
                table: "Korpa",
                newName: "KorpaID");
        }
    }
}
