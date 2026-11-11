using TrabajoPracticoDatos.Data;
using TrabajoPracticoDatos.Interfaces;
using TrabajoPracticoDatos.Models;

namespace TrabajoPracticoDatos.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly TiendaContext _context;

        public UsuarioRepository(TiendaContext context)
        {
            _context = context;
        }

        public List<Usuario> ObtenerTodos()
        {
            return _context.Usuarios.OrderBy(u => u.Nombre).ToList();
        }

        public Usuario? ObtenerPorId(int id)
        {
            return _context.Usuarios.Find(id);
        }

        public Usuario? ObtenerPorEmail(string email)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == email);
        }

        public void Agregar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public void CambiarRol(int id, string rol)
        {
            Usuario? usuario = _context.Usuarios.Find(id);

            if (usuario != null)
            {
                usuario.Rol = rol;
                _context.SaveChanges();
            }
        }
    }
}
