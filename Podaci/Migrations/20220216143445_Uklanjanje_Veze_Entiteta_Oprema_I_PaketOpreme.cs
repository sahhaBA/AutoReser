using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class Uklanjanje_Veze_Entiteta_Oprema_I_PaketOpreme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Oprema_PaketiOpreme_PaketOpremeID",
                table: "Oprema");

            migrationBuilder.DropIndex(
                name: "IX_Oprema_PaketOpremeID",
                table: "Oprema");

            migrationBuilder.DropColumn(
                name: "PaketOpremeID",
                table: "Oprema");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaketOpremeID",
                table: "Oprema",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Oprema_PaketOpremeID",
                table: "Oprema",
                column: "PaketOpremeID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Oprema_PaketiOpreme_PaketOpremeID",
                table: "Oprema",
                column: "PaketOpremeID",
                principalTable: "PaketiOpreme",
                principalColumn: "PaketOpremeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
