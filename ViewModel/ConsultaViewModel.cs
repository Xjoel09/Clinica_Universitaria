using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MedicalUTP.Models;
using MedicalUTP.DataAcess;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MedicalUTP.ViewModel
{
    public partial class ConsultaViewModel : ObservableObject
    {
        private readonly MedicalUTPDbContext _context;
        [ObservableProperty]
        private string tipoConsulta;
        [ObservableProperty]
        private DateTime fechaSeleccionada = DateTime.Now;
        [ObservableProperty]
        private TimeSpan horaSeleccionada;
        [ObservableProperty]
        private string motivoConsulta;
        [ObservableProperty]
        private string medicoAsignado;
        public List<string> TiposConsultas { get; } = new List<string>
        {
            "Consultas y evaluaciones médicas con previa cita",
            "Consultas y evaluaciones de urgencias",
            "Referencias a especialidades médicas",
            "Certificado de buena salud",
            "Solicitudes de estudios de gabinete",
            "Administración gratuita de medicamentos básicos",
            "Curaciones y corte de puntos",
            "Control de peso y talla",
            "Control de presión arterial",
            "Inhaloterapias",
            "Aplicación de medicamentos inyectables",
            "Toma de glicemia capilar"
        };

        public List<string> MedicosAsignados { get; } = new List<string>
        {
            "Dr Juanes Aguilar",
            "Dr Acuña Chapulini",
            "Dr Vellonicimo Cordoba"
        };
        public List<TimeSpan> HorasDisponibles { get; } = new List<TimeSpan>
        {
            new TimeSpan(10, 0, 0),
            new TimeSpan(11, 0, 0),
            new TimeSpan(12, 0, 0),
            new TimeSpan(16, 0, 0)
        };
        public ConsultaViewModel(MedicalUTPDbContext context)
        {
            _context = context;
        }
        [RelayCommand]
        private async Task AgendarCita()
        {
            if (string.IsNullOrWhiteSpace(TipoConsulta) || string.IsNullOrWhiteSpace(MotivoConsulta))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Por favor, complete todos los campos", "OK");
                return;
            }
            try
            {

                var nuevaCita = new Cita
                {
                    TipoConsulta = TipoConsulta,
                    FechaHora = FechaSeleccionada.Date + HoraSeleccionada,
                    MotivoConsulta = MotivoConsulta,
                    MedicoAsignado = MedicoAsignado
                };

                _context.Citas.Add(nuevaCita);
                await _context.SaveChangesAsync();

                await Application.Current.MainPage.DisplayAlert("Cita Agendada",
                    $"Su cita ha sido agendada para el {nuevaCita.FechaHora:dd/MM/yyyy} a las {nuevaCita.FechaHora:HH:mm}",
                    "OK");

                
                TipoConsulta = string.Empty;
                FechaSeleccionada = DateTime.Now;
                HoraSeleccionada = TimeSpan.Zero;
                MedicoAsignado = string.Empty;
                MotivoConsulta = string.Empty;
            }
            catch (DbUpdateException dbEx)
            {
                var innerException = dbEx.InnerException?.Message ?? "No hay detalles adicionales.";
                Debug.WriteLine($"Error de actualización en la base de datos: {innerException}");
                await Application.Current.MainPage.DisplayAlert("Error", $"No se pudo agendar la cita. Por favor, inténtelo de nuevo.{innerException}", "OK");
            }
        }
    }
}
