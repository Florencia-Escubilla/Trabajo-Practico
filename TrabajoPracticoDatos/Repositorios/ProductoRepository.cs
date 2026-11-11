using Microsoft.EntityFrameworkCore;
using TrabajoPracticoDatos.Data;
using TrabajoPracticoDatos.Interfaces;
using TrabajoPracticoDatos.Models;

namespace TrabajoPracticoDatos.Repositorios
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly TiendaContext _context;

        public ProductoRepository(TiendaContext context)
        {
            _context = context;
        }

        public List<Producto> ObtenerParaCatalogo(int? categoriaId)
        {
            IQueryable<Producto> consulta = _context.Productos
                .Include(p => p.Categoria)
                .Where(p => p.Activo && p.Categoria!.Activa);

            if (categoriaId != null)
            {
                consulta = consulta.Where(p => p.CategoriaId == categoriaId);
            }

            return consulta.OrderBy(p => p.Nombre).ToList();
        }

        public List<Producto> ObtenerTodos()
        {
            return _context.Productos
                .Include(p => p.Categoria)
                .OrderBy(p => p.Nombre)
                .ToList();
        }

        public Producto? ObtenerPorId(int id)
        {
            return _context.Productos.Find(id);
        }

        public void Agregar(Producto producto)
        {
            _context.Productos.Add(producto);
            _context.SaveChanges();
        }

        public void Actualizar(Producto producto)
        {
            _context.Productos.Update(producto);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            Producto? producto = _context.Productos.Find(id);

            if (producto != null)
            {
                producto.Activo = false;
                _context.SaveChanges();
            }
        }

        public void Reactivar(int id)
        {
            Producto? producto = _context.Productos.Find(id);

            if (producto != null)
            {
                producto.Activo = true;
                _context.SaveChanges();
            }
        }
    }
}
