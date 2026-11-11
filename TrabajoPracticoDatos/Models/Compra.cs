using System.ComponentModel.DataAnnotations.Schema;

namespace TrabajoPracticoDatos.Models
{
    public class Compra
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }

        public int ProductoId { get; set; }

        public Producto? Producto { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Precio { get; set; }

        public DateTime Fecha { get; set; }

        public bool Vista { get; set; }
    }
}
