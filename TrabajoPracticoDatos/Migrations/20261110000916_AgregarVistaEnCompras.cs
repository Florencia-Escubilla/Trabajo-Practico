using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrabajoPracticoDatos.Migrations
{
    public partial class AgregarVistaEnCompras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Vista",
                table: "Compras",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vista",
                table: "Compras");
        }
    }
}
