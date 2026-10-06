using System.ComponentModel.DataAnnotations;

namespace Trabajo_Practico.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; set; } = "";

        [Display(Name = "Descripción")]
        [StringLength(250, ErrorMessage = "La descripción no puede superar los 250 caracteres.")]
        public string? Descripcion { get; set; }

        public bool Activa { get; set; }
    }
}