using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using MedicalUTP.Models;
using MedicalUTP.DataAcess;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MedicalUTP.ViewsModel
{
    public partial class HistorialCitasViewModel : ObservableObject
    {
        private readonly MedicalUTPDbContext _context;
        [ObservableProperty]
        private ObservableCollection<Cita> citasPasadas;
        [ObservableProperty]
        private ObservableCollection<Cita> citasFuturas;
        public HistorialCitasViewModel(MedicalUTPDbContext context)
        {
            _context = context;
            ActualizarHistorial();
        }
        [RelayCommand]
        private async Task ActualizarHistorial()
        {
            try
            {
                var ahora = DateTime.Now;
                var todasLasCitas = await _context.Citas.ToListAsync();
                Debug.WriteLine($"Número total de citas: {todasLasCitas.Count}");

                CitasPasadas = new ObservableCollection<Cita>(todasLasCitas.Where(c => c.FechaHora <= ahora));
                CitasFuturas = new ObservableCollection<Cita>(todasLasCitas.Where(c => c.FechaHora > ahora));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al actualizar el historial: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo cargar el historial de citas.", "OK");
            }
        }
    }
}

