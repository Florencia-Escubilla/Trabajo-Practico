using System.ComponentModel.DataAnnotations;

namespace TrabajoPracticoDatos.Models
{
    public class Usuario
    {
        public const string RolAdministrador = "Administrador";
        public const string RolCliente = "Cliente";

        public int Id { get; set; }

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El email no es válido.")]
        [StringLength(150, ErrorMessage = "El email no puede superar los 150 caracteres.")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; set; } = "";

        [Required(ErrorMessage = "El rol es obligatorio.")]
        [StringLength(20, ErrorMessage = "El rol no puede superar los 20 caracteres.")]
        public string Rol { get; set; } = RolCliente;
    }
}
