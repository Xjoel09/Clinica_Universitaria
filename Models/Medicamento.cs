using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
