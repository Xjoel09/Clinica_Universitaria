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

        public bool IsPatientSelected => SelectedPatient != null;

        public HistorialDoctorViewModel(MedicalUTPDbContext context)
        {
            _context = context;
            LoadPatients();
        }

        private async void LoadPatients()
        {
            var users = await _context.User
                .Where(u => u.Role == "Estudiante" || u.Role == "Docente" || u.Role == "Administrativo")
                .ToListAsync();
            Patients = new ObservableCollection<User>(users);
        }

        partial void OnSelectedPatientChanged(User value)
        {
            OnPropertyChanged(nameof(IsPatientSelected));
        }
    }
}

