using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Trabajo_Practico.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Activa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Talle = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ImagenUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Activa", "Descripcion", "Nombre" },
                values: new object[,]
                {
                    { 1, true, "Remeras de algodón Remeras musculosas Bodys manga corta", "Remeras" },
                    { 2, true, "Sastreros del 36 al 46 Jeans Mom 36 al 46 Jeans Oxford 36 al 46 Calzas 36 al 46", "Pantalones (mujer)" },
                    { 3, true, "Buzos Básicos 1 al 6 Buzos Oversize (Talle Único) Buzos Cortos (Talle Único)", "Buzos" },
                    { 4, true, "Minis Shorts 36 al 42 Shorts básicos 36 al 46 Polleras mini (talle 1 al 3)", "Shorts, polleras de mujer" },
                    { 5, true, "Conjunto deportivo de Argentina(solo disponible talle 1 y 2)", "Ropa deportiva" }
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "Activo", "CategoriaId", "Color", "Descripcion", "ImagenUrl", "Nombre", "Precio", "Stock", "Talle" },
                values: new object[,]
                {
                    { 1, true, 1, "Blanco", "Remera de algodón lisa", null, "Remera básica", 15000m, 10, "M" },
                    { 2, true, 2, "Celeste", "Jean de tiro alto", null, "Jean Mom", 42000m, 5, "38" },
                    { 3, true, 3, "Rosa", "Buzo con capucha", null, "Buzo oversize", 38000m, 0, "Único" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CategoriaId",
                table: "Productos",
                column: "CategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
