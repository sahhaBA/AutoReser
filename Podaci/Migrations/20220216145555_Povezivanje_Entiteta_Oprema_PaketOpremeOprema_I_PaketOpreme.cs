using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class Povezivanje_Entiteta_Oprema_PaketOpremeOprema_I_PaketOpreme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaketOpremeOprema",
                columns: table => new
                {
                    OpremaID = table.Column<int>(type: "int", nullable: false),
                    PaketOpremeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaketOpremeOprema", x => new { x.OpremaID, x.PaketOpremeID });
                    table.ForeignKey(
                        name: "FK_PaketOpremeOprema_Oprema_OpremaID",
                        column: x => x.OpremaID,
                        principalTable: "Oprema",
                        principalColumn: "OpremaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaketOpremeOprema_PaketiOpreme_PaketOpremeID",
                        column: x => x.PaketOpremeID,
                        principalTable: "PaketiOpreme",
                        principalColumn: "PaketOpremeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaketOpremeOprema_PaketOpremeID",
                table: "PaketOpremeOprema",
                column: "PaketOpremeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaketOpremeOprema");
        }
    }
}
