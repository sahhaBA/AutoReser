using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class Update_Tipa_Atributa_AutomobilID_Entiteta_Izvjestaj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AutomobilID",
                table: "Izvjestaji",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AutomobilID",
                table: "Izvjestaji",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
