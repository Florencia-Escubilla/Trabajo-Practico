using TrabajoPracticoDatos.Models;

namespace TrabajoPracticoDatos.Interfaces
{
    public interface IProductoRepository
    {
        List<Producto> ObtenerParaCatalogo(int? categoriaId);

        List<Producto> ObtenerTodos();

        Producto? ObtenerPorId(int id);

        void Agregar(Producto producto);

        void Actualizar(Producto producto);

        void Eliminar(int id);

        void Reactivar(int id);
    }
}
