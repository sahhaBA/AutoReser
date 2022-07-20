using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class Dodavanje_Entiteta_Slike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Slike",
                columns: table => new
                {
                    SlikeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AutomobilID = table.Column<int>(type: "int", nullable: false),
                    NazivSlike1 = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    NazivSlike2 = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    NazivSlike3 = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    NazivSlike4 = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    NazivSlike5 = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slike", x => x.SlikeID);
                    table.ForeignKey(
                        name: "FK_Slike_Automobili_AutomobilID",
                        column: x => x.AutomobilID,
                        principalTable: "Automobili",
                        principalColumn: "AutomobilID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Slike_AutomobilID",
                table: "Slike",
                column: "AutomobilID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Slike");
        }
    }
}
