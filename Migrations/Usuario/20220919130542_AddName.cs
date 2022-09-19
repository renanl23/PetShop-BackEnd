using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetShop_BackEnd.Migrations.Usuario
{
    public partial class AddName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Usuarios",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "Usuarios");
        }
    }
}
