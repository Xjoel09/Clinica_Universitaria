using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalUTP.Models
{
    public class Cita
    {
        [Key]
        public int Id { get; set; }
        public required  string TipoConsulta { get; set; }
        public DateTime FechaHora { get; set; }
        public required string MedicoAsignado { get; set; }
        public required string MotivoConsulta { get; set; }
        public bool EsFutura => FechaHora > DateTime.Now;
    }
    //{
    //    [Key]
    //    public int IdCita { get; set; }
    //    public required int UsuarioId { get; set; }
    //    [ForeignKey("IdUser")]
    //    public required User User { get; set; }
    //   // public required virtual User Refuser { get; set; }
    //    public required string TipoConsulta { get; set; }
    //    public DateTime FechaHora { get; set; }
    //    public required string MedicoAsignado { get; set; }
    //    public required string MotivoConsulta { get; set; }


    //}

}
