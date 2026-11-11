using Microsoft.EntityFrameworkCore;
using TrabajoPracticoDatos.Data;
using TrabajoPracticoDatos.Interfaces;
using TrabajoPracticoDatos.Models;

namespace TrabajoPracticoDatos.Repositorios
{
    public class CompraRepository : ICompraRepository
    {
        private readonly TiendaContext _context;

        public CompraRepository(TiendaContext context)
        {
            _context = context;
        }

        public Compra? Comprar(int productoId, int usuarioId)
        {
            Producto? producto = _context.Productos.Find(productoId);

            if (producto == null || !producto.Activo || producto.Stock <= 0)
            {
                return null;
            }

            producto.Stock = producto.Stock - 1;

            Compra compra = new Compra
            {
                UsuarioId = usuarioId,
                ProductoId = productoId,
                Producto = producto,
                Precio = producto.Precio,
                Fecha = DateTime.Now
            };

            _context.Compras.Add(compra);
            _context.SaveChanges();
            return compra;
        }

        public List<Compra> ObtenerPorUsuario(int usuarioId)
        {
            return _context.Compras
                .Include(c => c.Producto)
                .Where(c => c.UsuarioId == usuarioId)
                .OrderByDescending(c => c.Fecha)
                .ToList();
        }

        public List<Compra> ObtenerTodas()
        {
            return _context.Compras
                .Include(c => c.Usuario)
                .Include(c => c.Producto)
                .OrderByDescending(c => c.Fecha)
                .ToList();
        }

        public int ContarNoVistas()
        {
            return _context.Compras.Count(c => !c.Vista);
        }

        public void MarcarTodasComoVistas()
        {
            List<Compra> noVistas = _context.Compras.Where(c => !c.Vista).ToList();

            foreach (Compra compra in noVistas)
            {
                compra.Vista = true;
            }

            _context.SaveChanges();
        }
    }
}
