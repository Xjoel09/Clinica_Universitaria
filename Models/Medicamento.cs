using System.ComponentModel.DataAnnotations;

namespace MedicalUTP.Models
{
    public class Medicamento
    {
        [Key]
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required int Cantidad { get; set; }
        public required decimal Precio { get; set; }
    }
}