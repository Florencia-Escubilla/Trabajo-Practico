using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrabajoPracticoDatos.Models;
using TrabajoPracticoDatos.Interfaces;

namespace Trabajo_Practico.Controllers
{
    [Authorize]
    public class CatalogoController : Controller
    {
        private readonly IProductoRepository _productoRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public CatalogoController(IProductoRepository productoRepository, ICategoriaRepository categoriaRepository)
        {
            _productoRepository = productoRepository;
            _categoriaRepository = categoriaRepository;
        }

        public IActionResult Index(int? categoriaId)
        {
            List<Producto> productos = _productoRepository.ObtenerParaCatalogo(categoriaId);

            ViewBag.Categorias = _categoriaRepository.ObtenerTodas().Where(c => c.Activa).ToList();
            ViewBag.CategoriaElegida = categoriaId;

            return View(productos);
        }
    }
}
