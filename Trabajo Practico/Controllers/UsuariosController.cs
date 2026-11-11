using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrabajoPracticoDatos.Models;
using TrabajoPracticoDatos.Interfaces;

namespace Trabajo_Practico.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UsuariosController : Controller
    {
        private readonly IUsuarioRepository _repository;
        private readonly IConfiguration _configuration;

        public UsuariosController(IUsuarioRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            List<Usuario> usuarios = _repository.ObtenerTodos();
            ViewBag.EmailAdministradorPrincipal = _configuration["AdministradorPrincipal"];
            return View(usuarios);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AsignarAdministrador(int id)
        {
            Usuario? usuario = _repository.ObtenerPorId(id);

            if (usuario == null)
            {
                return NotFound();
            }

            _repository.CambiarRol(id, Usuario.RolAdministrador);
            TempData["Mensaje"] = $"{usuario.Nombre} ahora es administrador. Va a ver el cambio la próxima vez que inicie sesión.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult QuitarAdministrador(int id)
        {
            Usuario? usuario = _repository.ObtenerPorId(id);

            if (usuario == null)
            {
                return NotFound();
            }

            string? miEmail = User.FindFirstValue(ClaimTypes.Email);

            if (usuario.Email == miEmail)
            {
                TempData["Error"] = "No podés quitarte el rol de administrador a vos mismo.";
                return RedirectToAction(nameof(Index));
            }

            if (string.Equals(usuario.Email, _configuration["AdministradorPrincipal"], StringComparison.OrdinalIgnoreCase))
            {
                TempData["Error"] = "No se le puede quitar el rol al administrador principal de la tienda.";
                return RedirectToAction(nameof(Index));
            }

            _repository.CambiarRol(id, Usuario.RolCliente);
            TempData["Mensaje"] = $"{usuario.Nombre} ahora es cliente.";
            return RedirectToAction(nameof(Index));
        }
    }
}
