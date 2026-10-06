using Microsoft.EntityFrameworkCore;
using Trabajo_Practico.Models;

namespace Trabajo_Practico.Data
{
    public class TiendaContext : DbContext
    {
        public TiendaContext(DbContextOptions<TiendaContext> options)
            : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Producto> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { Id = 1, Nombre = "Remeras", Descripcion = "Remeras de algodón Remeras musculosas Bodys manga corta", Activa = true },
                new Categoria { Id = 2, Nombre = "Pantalones (mujer)", Descripcion = "Sastreros del 36 al 46 Jeans Mom 36 al 46 Jeans Oxford 36 al 46 Calzas 36 al 46", Activa = true },
                new Categoria { Id = 3, Nombre = "Buzos", Descripcion = "Buzos Básicos 1 al 6 Buzos Oversize (Talle Único) Buzos Cortos (Talle Único)", Activa = true },
                new Categoria { Id = 4, Nombre = "Shorts, polleras de mujer", Descripcion = "Minis Shorts 36 al 42 Shorts básicos 36 al 46 Polleras mini (talle 1 al 3)", Activa = true },
                new Categoria { Id = 5, Nombre = "Ropa deportiva", Descripcion = "Conjunto deportivo de Argentina(solo disponible talle 1 y 2)", Activa = true }
            );

            modelBuilder.Entity<Producto>().HasData(
                new Producto { Id = 1, Nombre = "Remera básica", Descripcion = "Remera de algodón lisa", Precio = 15000m, Stock = 10, Talle = "M", Color = "Blanco", CategoriaId = 1, Activo = true },
                new Producto { Id = 2, Nombre = "Jean Mom", Descripcion = "Jean de tiro alto", Precio = 42000m, Stock = 5, Talle = "38", Color = "Celeste", CategoriaId = 2, Activo = true },
                new Producto { Id = 3, Nombre = "Buzo oversize", Descripcion = "Buzo con capucha", Precio = 38000m, Stock = 0, Talle = "Único", Color = "Rosa", CategoriaId = 3, Activo = true }
            );
        }
    }
}