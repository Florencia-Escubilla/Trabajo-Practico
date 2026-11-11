using TrabajoPracticoDatos.Models;

namespace TrabajoPracticoDatos.Interfaces
{
    public interface ICompraRepository
    {
        Compra? Comprar(int productoId, int usuarioId);

        List<Compra> ObtenerPorUsuario(int usuarioId);

        List<Compra> ObtenerTodas();

        int ContarNoVistas();

        void MarcarTodasComoVistas();
    }
}
