using TrabajoPracticoDatos.Models;

namespace TrabajoPracticoDatos.Interfaces
{
    public interface ICategoriaRepository
    {
        List<Categoria> ObtenerTodas();

        Categoria? ObtenerPorId(int id);

        void Agregar(Categoria categoria);

        void Actualizar(Categoria categoria);

        void Eliminar(int id);

        void Reactivar(int id);
    }
}