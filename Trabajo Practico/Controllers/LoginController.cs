using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using TrabajoPracticoDatos.Models;
using TrabajoPracticoDatos.Interfaces;

namespace Trabajo_Practico.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public LoginController(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public async Task Login()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse")
            });
        }

        public async Task<IActionResult> GoogleResponse(string? returnUrl = null)
        {
            AuthenticateResult resultado = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!resultado.Succeeded || resultado.Principal == null)
            {
                return RedirectToAction(nameof(Index));
            }

            string? email = resultado.Principal.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrEmpty(email))
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction(nameof(Index));
            }

            string nombre = resultado.Principal.FindFirstValue(ClaimTypes.Name) ?? email;

            string? emailAdministrador = _configuration["AdministradorPrincipal"];
            bool esAdministradorPrincipal = string.Equals(email, emailAdministrador, StringComparison.OrdinalIgnoreCase);

            Usuario? usuario = _usuarioRepository.ObtenerPorEmail(email);

            if (usuario == null)
            {
                usuario = new Usuario
                {
                    Email = email,
                    Nombre = nombre,
                    Rol = esAdministradorPrincipal ? Usuario.RolAdministrador : Usuario.RolCliente
                };
                _usuarioRepository.Agregar(usuario);
            }
            else if (esAdministradorPrincipal && usuario.Rol != Usuario.RolAdministrador)
            {
                _usuarioRepository.CambiarRol(usuario.Id, Usuario.RolAdministrador);
                usuario.Rol = Usuario.RolAdministrador;
            }

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };

            ClaimsIdentity identidad = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identidad));

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccesoDenegado()
        {
            return View();
        }
    }
}
