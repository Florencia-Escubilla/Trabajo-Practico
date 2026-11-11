using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrabajoPracticoDatos.Interfaces;
using TrabajoPracticoDatos.Models;

namespace Trabajo_Practico.Controllers
{
    [Authorize]
    public class ComprasController : Controller
    {
        private readonly ICompraRepository _compraRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public ComprasController(ICompraRepository compraRepository, IUsuarioRepository usuarioRepository)
        {
            _compraRepository = compraRepository;
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Cliente")]
        public IActionResult Comprar(int productoId, int? categoriaId)
        {
            Usuario? usuario = ObtenerUsuarioActual();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Login");
            }

            Compra? compra = _compraRepository.Comprar(productoId, usuario.Id);

            if (compra == null)
            {
                TempData["Error"] = "No se pudo realizar la compra: el producto ya no tiene stock.";
            }
            else
            {
                TempData["Mensaje"] = $"¡Compra realizada con éxito! Compraste {compra.Producto?.Nombre}.";
            }

            return RedirectToAction("Index", "Catalogo", new { categoriaId });
        }

        [Authorize(Roles = "Cliente")]
        public IActionResult MisCompras()
        {
            Usuario? usuario = ObtenerUsuarioActual();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Login");
            }

            List<Compra> compras = _compraRepository.ObtenerPorUsuario(usuario.Id);
            return View(compras);
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Ventas()
        {
            List<Compra> ventas = _compraRepository.ObtenerTodas();
            List<int> idsNuevas = ventas.Where(v => !v.Vista).Select(v => v.Id).ToList();

            _compraRepository.MarcarTodasComoVistas();

            ViewBag.IdsNuevas = idsNuevas;
            return View(ventas);
        }

        private Usuario? ObtenerUsuarioActual()
        {
            string? email = User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrEmpty(email))
            {
                return null;
            }

            return _usuarioRepository.ObtenerPorEmail(email);
        }
    }
}
