using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trabajo_Practico.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; set; } = "";

        [Display(Name = "Descripción")]
        [StringLength(500, ErrorMessage = "La descripción no puede superar los 500 caracteres.")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(1, 10000000, ErrorMessage = "El precio debe ser mayor a 0.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El stock es obligatorio.")]
        [Range(0, 100000, ErrorMessage = "El stock no puede ser negativo.")]
        public int Stock { get; set; }

        [StringLength(20, ErrorMessage = "El talle no puede superar los 20 caracteres.")]
        public string? Talle { get; set; }

        [StringLength(50, ErrorMessage = "El color no puede superar los 50 caracteres.")]
        public string? Color { get; set; }

        [Display(Name = "Imagen (URL)")]
        [StringLength(500, ErrorMessage = "La URL no puede superar los 500 caracteres.")]
        public string? ImagenUrl { get; set; }

        [Display(Name = "Categoría")]
        [Range(1, int.MaxValue, ErrorMessage = "Tenés que elegir una categoría.")]
        public int CategoriaId { get; set; }

        public Categoria? Categoria { get; set; }

        public bool Activo { get; set; }
    }
}