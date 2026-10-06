using Trabajo_Practico.Models;

namespace Trabajo_Practico.Repositories
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