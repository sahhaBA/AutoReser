using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class izmejna_strucne_soreme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uposlenici_StrurucnaSprema_StrurucnaSpremaStrucnaSpremaID",
                table: "Uposlenici");

            migrationBuilder.DropTable(
                name: "StrurucnaSprema");

            migrationBuilder.DropIndex(
                name: "IX_Uposlenici_StrurucnaSpremaStrucnaSpremaID",
                table: "Uposlenici");

            migrationBuilder.DropColumn(
                name: "StrurucnaSpremaStrucnaSpremaID",
                table: "Uposlenici");

            migrationBuilder.CreateTable(
                name: "StrucnaSprema",
                columns: table => new
                {
                    StrucnaSpremaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrucnaSprema", x => x.StrucnaSpremaID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Uposlenici_StrucnaSpremaID",
                table: "Uposlenici",
                column: "StrucnaSpremaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Uposlenici_StrucnaSprema_StrucnaSpremaID",
                table: "Uposlenici",
                column: "StrucnaSpremaID",
                principalTable: "StrucnaSprema",
                principalColumn: "StrucnaSpremaID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uposlenici_StrucnaSprema_StrucnaSpremaID",
                table: "Uposlenici");

            migrationBuilder.DropTable(
                name: "StrucnaSprema");

            migrationBuilder.DropIndex(
                name: "IX_Uposlenici_StrucnaSpremaID",
                table: "Uposlenici");

            migrationBuilder.AddColumn<int>(
                name: "StrurucnaSpremaStrucnaSpremaID",
                table: "Uposlenici",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StrurucnaSprema",
                columns: table => new
                {
                    StrucnaSpremaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrurucnaSprema", x => x.StrucnaSpremaID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Uposlenici_StrurucnaSpremaStrucnaSpremaID",
                table: "Uposlenici",
                column: "StrurucnaSpremaStrucnaSpremaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Uposlenici_StrurucnaSprema_StrurucnaSpremaStrucnaSpremaID",
                table: "Uposlenici",
                column: "StrurucnaSpremaStrucnaSpremaID",
                principalTable: "StrurucnaSprema",
                principalColumn: "StrucnaSpremaID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
