using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalUTP.Models
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        public required string Nombre { get; set; }
        public required string Telefono { get; set; }
        public required string Cedula { get; set; }
        public required string Correo { get; set; }
        public required string Password { get; set; }
        public required string Role { get; set; }
        // Nueva propiedad para almacenar el historial médico
        public string Historial { get; set; } = string.Empty;
    }
}


