using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MedicalUTP.Models;
using MedicalUTP.DataAcess;
using Microsoft.EntityFrameworkCore;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MedicalUTP.ViewModel
{
    public partial class HistorialDoctorViewModel : ObservableObject
    {
        private readonly MedicalUTPDbContext _context;

        [ObservableProperty]
        private ObservableCollection<User> patients;

        [ObservableProperty]
        private User selectedPatient;

        [ObservableProperty]
        private string historialTexto; // Propiedad para el historial editable

        public bool IsPatientSelected => SelectedPatient != null;

        public HistorialDoctorViewModel(MedicalUTPDbContext context)
        {
            _context = context;
            LoadPatients();
            HistorialTexto = string.Empty; // Inicializa el historial vacío o carga de base de datos
        }

        private async void LoadPatients()
        {
            var users = await _context.User
                .Where(u => u.Role == "Estudiante" || u.Role == "Docente" || u.Role == "Admin")
                .ToListAsync();
            Patients = new ObservableCollection<User>(users);
        }

        partial void OnSelectedPatientChanged(User value)
        {
            OnPropertyChanged(nameof(IsPatientSelected));

            if (value != null)
            {
                HistorialTexto = value.Historial ?? string.Empty; // Carga historial del paciente
            }
        }

        public async Task GuardarHistorialAsync()
        {
            if (SelectedPatient != null)
            {
                SelectedPatient.Historial = HistorialTexto; // Guarda el texto actual en el historial del paciente
                _context.Update(SelectedPatient);
                await _context.SaveChangesAsync();
            }
        }
    }
}






