using TrabajoPracticoDatos.Models;

namespace TrabajoPracticoDatos.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> ObtenerTodos();

        Usuario? ObtenerPorId(int id);

        Usuario? ObtenerPorEmail(string email);

        void Agregar(Usuario usuario);

        void CambiarRol(int id, string rol);
    }
}
