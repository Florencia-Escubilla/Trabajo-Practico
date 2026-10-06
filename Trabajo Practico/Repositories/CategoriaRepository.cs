using Trabajo_Practico.Data;
using Trabajo_Practico.Models;

namespace Trabajo_Practico.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly TiendaContext _context;

        public CategoriaRepository(TiendaContext context)
        {
            _context = context;
        }

        public List<Categoria> ObtenerTodas()
        {
            return _context.Categorias.ToList();
        }

        public Categoria? ObtenerPorId(int id)
        {
            return _context.Categorias.Find(id);
        }

        public void Agregar(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
        }

        public void Actualizar(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            Categoria? categoria = _context.Categorias.Find(id);

            if (categoria != null)
            {
                categoria.Activa = false;
                _context.SaveChanges();
            }
        }

        public void Reactivar(int id)
        {
            Categoria? categoria = _context.Categorias.Find(id);

            if (categoria != null)
            {
                categoria.Activa = true;
                _context.SaveChanges();
            }
        }
    }
}