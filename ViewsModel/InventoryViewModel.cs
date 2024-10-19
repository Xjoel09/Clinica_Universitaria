using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MedicalUTP.DataAcess;
using MedicalUTP.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalUTP.ViewsModel
{
    public partial class InventarioViewModel : ObservableObject
    {
        private readonly MedicalUTPDbContext _context;

        [ObservableProperty]
        private ObservableCollection<Inventario> _medicamentos;

        [ObservableProperty]
        private Inventario _selectedMedicamento;

        [ObservableProperty]
        private bool _isEditPopupVisible;

        public InventarioViewModel(MedicalUTPDbContext context)
        {
            _context = context;
            LoadMedicamentos();
        }

        [RelayCommand]
        private async Task LoadMedicamentos()
        {
            var medicamentosList = await _context.Inventario.ToListAsync();
            Medicamentos = new ObservableCollection<Inventario>(medicamentosList);
        }

        [RelayCommand]
        public async void DeleteMedicamento(Inventario medicamento)
        {
            if (medicamento != null)
            {
                _context.Inventario.Remove(medicamento);
                await _context.SaveChangesAsync();
                Medicamentos.Remove(medicamento);
            }
        }

        [RelayCommand]
        public void EditMedicamento(Inventario medicamento)
        {
            if (medicamento != null)
            {
                SelectedMedicamento = new Inventario
                {
                    IdInventario = medicamento.IdInventario,
                    Nombre = medicamento.Nombre,
                    Descripcion = medicamento.Descripcion,
                    Cantidad = medicamento.Cantidad,
                    Precio = medicamento.Precio
                };
                IsEditPopupVisible = true; 
            }
        }

        [RelayCommand]
        public async void SaveMedicamentoChanges()
        {
            if (SelectedMedicamento != null)
            {
                var medicamentoToUpdate = await _context.Inventario.FindAsync(SelectedMedicamento.IdInventario);
                if (medicamentoToUpdate != null)
                {
                    medicamentoToUpdate.Nombre = SelectedMedicamento.Nombre;
                    medicamentoToUpdate.Descripcion = SelectedMedicamento.Descripcion;
                    medicamentoToUpdate.Cantidad = SelectedMedicamento.Cantidad;
                    medicamentoToUpdate.Precio = SelectedMedicamento.Precio;

                    await _context.SaveChangesAsync();
                    await LoadMedicamentos();
                    IsEditPopupVisible = false;
                }
            }
        }

        [RelayCommand]
        public void CancelEdit()
        {
            IsEditPopupVisible = false;
        }
    }
}