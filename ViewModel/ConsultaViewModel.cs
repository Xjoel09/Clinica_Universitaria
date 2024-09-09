using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace MedicalUTP.ViewModel
{
    public partial class ConsultaViewModel : ObservableObject
    {
        [ObservableProperty]
        private string tipoConsulta;

        [ObservableProperty]
        private string diaSeleccionado;

        [ObservableProperty]
        private string horaSeleccionada;

        public List<string> TiposConsultas { get; set; } = new List<string> { "Consultas y evaluaciones médicas con previa cita", "Consultas y evaluaciones de urgencias", "Referencias a especialidades médicas", "Certificado de buena salud", "Solicitudes de estudios de gabinete", "Administración gratuita de medicamentos básicos", "Curaciones y corte de puntos", "Control de peso y talla", "Control de presión arterial", "Inhaloterapias", "Aplicación de medicamentos inyectables", "Toma de glicemia capilar"};
        public List<string> DiasDisponibles { get; set; } = new List<string> { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes" };
        public List<string> HorasDisponibles { get; set; } = new List<string> { "10:00 AM", "11:00 AM", "12:00 PM", "4:00 PM" };

        public ConsultaViewModel()
        {
        }

        [RelayCommand]
        private async Task OnSubmit()
        {
            var mainPage = Application.Current?.MainPage ?? throw new InvalidOperationException("No se pudo acceder a MainPage.");
            await mainPage.DisplayAlert("Solicitud Enviada",
                $"Consulta: {TipoConsulta}, Día: {DiaSeleccionado}, Hora: {HoraSeleccionada}",
                "OK");
        }
    }
}

