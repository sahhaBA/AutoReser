using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class Priprema_Entiteta_Korpa_Na_Identity_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KorpaID",
                table: "Korpa",
                newName: "AutomobilID1");

            migrationBuilder.AddColumn<int>(
                name: "AutomobilID",
                table: "Korpa",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Korpa",
                table: "Korpa",
                column: "AutomobilID");

            migrationBuilder.CreateIndex(
                name: "IX_Korpa_AutomobilID1",
                table: "Korpa",
                column: "AutomobilID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Korpa_Automobili_AutomobilID1",
                table: "Korpa",
                column: "AutomobilID1",
                principalTable: "Automobili",
                principalColumn: "AutomobilID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Korpa_Automobili_AutomobilID1",
                table: "Korpa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Korpa",
                table: "Korpa");

            migrationBuilder.DropIndex(
                name: "IX_Korpa_AutomobilID1",
                table: "Korpa");

            migrationBuilder.DropColumn(
                name: "AutomobilID",
                table: "Korpa");

            migrationBuilder.RenameColumn(
                name: "AutomobilID1",
                table: "Korpa",
                newName: "KorpaID");
        }
    }
}
