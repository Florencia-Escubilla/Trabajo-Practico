using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrabajoPracticoDatos.Models;
using TrabajoPracticoDatos.Interfaces;

namespace Trabajo_Practico.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ProductosController : Controller
    {
        private static readonly string[] ExtensionesPermitidas = { ".jpg", ".jpeg", ".png", ".webp" };
        private const long TamanioMaximo = 5 * 1024 * 1024;

        private readonly IProductoRepository _productoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IWebHostEnvironment _entorno;

        public ProductosController(IProductoRepository productoRepository, ICategoriaRepository categoriaRepository, IWebHostEnvironment entorno)
        {
            _productoRepository = productoRepository;
            _categoriaRepository = categoriaRepository;
            _entorno = entorno;
        }

        public IActionResult Index()
        {
            List<Producto> productos = _productoRepository.ObtenerTodos();
            return View(productos);
        }

        public IActionResult Create()
        {
            CargarCategorias(null);
            return View(new Producto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Producto producto, IFormFile? imagen)
        {
            ValidarImagen(imagen);

            if (!ModelState.IsValid)
            {
                CargarCategorias(producto.CategoriaId);
                return View(producto);
            }

            if (imagen != null)
            {
                producto.ImagenUrl = GuardarImagen(imagen);
            }

            producto.Activo = true;
            _productoRepository.Agregar(producto);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            Producto? producto = _productoRepository.ObtenerPorId(id);

            if (producto == null)
            {
                return NotFound();
            }

            CargarCategorias(producto.CategoriaId);
            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Producto producto, IFormFile? imagen, bool quitarImagen)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            ValidarImagen(imagen);

            if (!ModelState.IsValid)
            {
                CargarCategorias(producto.CategoriaId);
                return View(producto);
            }

            if (imagen != null)
            {
                BorrarImagen(producto.ImagenUrl);
                producto.ImagenUrl = GuardarImagen(imagen);
            }
            else if (quitarImagen)
            {
                BorrarImagen(producto.ImagenUrl);
                producto.ImagenUrl = null;
            }

            _productoRepository.Actualizar(producto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _productoRepository.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reactivar(int id)
        {
            _productoRepository.Reactivar(id);
            return RedirectToAction(nameof(Index));
        }

        private void CargarCategorias(int? categoriaElegida)
        {
            List<Categoria> categorias = _categoriaRepository.ObtenerTodas()
                .Where(c => c.Activa || c.Id == categoriaElegida)
                .ToList();

            ViewBag.Categorias = new SelectList(categorias, "Id", "Nombre", categoriaElegida);
        }

        private void ValidarImagen(IFormFile? imagen)
        {
            if (imagen == null)
            {
                return;
            }

            string extension = Path.GetExtension(imagen.FileName).ToLowerInvariant();

            if (!ExtensionesPermitidas.Contains(extension))
            {
                ModelState.AddModelError(string.Empty, "La imagen tiene que ser JPG, PNG o WEBP.");
            }

            if (imagen.Length > TamanioMaximo)
            {
                ModelState.AddModelError(string.Empty, "La imagen no puede pesar más de 5 MB.");
            }
        }

        private string GuardarImagen(IFormFile imagen)
        {
            string carpeta = Path.Combine(_entorno.WebRootPath, "img", "productos");
            Directory.CreateDirectory(carpeta);

            string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(imagen.FileName).ToLowerInvariant();
            string rutaCompleta = Path.Combine(carpeta, nombreArchivo);

            using (FileStream archivo = new FileStream(rutaCompleta, FileMode.Create))
            {
                imagen.CopyTo(archivo);
            }

            return "/img/productos/" + nombreArchivo;
        }

        private void BorrarImagen(string? imagenUrl)
        {
            if (string.IsNullOrEmpty(imagenUrl) || !imagenUrl.StartsWith("/img/productos/"))
            {
                return;
            }

            string ruta = Path.Combine(_entorno.WebRootPath, "img", "productos", Path.GetFileName(imagenUrl));

            if (System.IO.File.Exists(ruta))
            {
                System.IO.File.Delete(ruta);
            }
        }
    }
}
