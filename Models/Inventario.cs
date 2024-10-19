using System.ComponentModel.DataAnnotations;

namespace MedicalUTP.Models
{
    public class Inventario
    {
        [Key]
        public int IdInventario { get; set; }
        public required string Nombre { get; set; }
        public required string Descripcion { get; set; }
        public required int Cantidad { get; set; }
        public required int Precio { get; set; }
    }
}
