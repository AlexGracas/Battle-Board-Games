using Microsoft.EntityFrameworkCore.Migrations;

namespace ModelBattleBoardGames.Migrations
{
    public partial class Nomes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Usuarios",
                newName: "SobreNome");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Usuarios",
                newName: "PrimeiroNome");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SobreNome",
                table: "Usuarios",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "PrimeiroNome",
                table: "Usuarios",
                newName: "FirstName");
        }
    }
}
