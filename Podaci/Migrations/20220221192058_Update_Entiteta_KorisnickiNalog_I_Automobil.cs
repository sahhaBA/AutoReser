using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class Update_Entiteta_KorisnickiNalog_I_Automobil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Korpa",
                columns: table => new
                {
                    KorisnickiNalogID = table.Column<int>(type: "int", nullable: false),
                    AutomobilID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korpa", x => new { x.KorisnickiNalogID, x.AutomobilID });
                    table.ForeignKey(
                        name: "FK_Korpa_Automobili_AutomobilID",
                        column: x => x.AutomobilID,
                        principalTable: "Automobili",
                        principalColumn: "AutomobilID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Korpa_KorisnickiNalog_KorisnickiNalogID",
                        column: x => x.KorisnickiNalogID,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "KorisnickiNalogID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Korpa_AutomobilID",
                table: "Korpa",
                column: "AutomobilID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Korpa");
        }
    }
}
