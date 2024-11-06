using MedicalUTP.DataAcess;
using MedicalUTP.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace MedicalUTP.Pages
{
    public partial class Inventario : ContentPage
    {
        private readonly MedicalUTPDbContext _context;
        public ObservableCollection<Medicamento> Medicamentos { get; set; }
        public ObservableCollection<Medicamento> MedicamentosCaducados { get; set; } // Nueva colección para medicamentos caducados o próximos a caducar
        private bool _isMenuOpen;

        public ICommand EditarCommand { get; }
        public ICommand EliminarCommand { get; }

        public Inventario(MedicalUTPDbContext context)
        {
            InitializeComponent();
            _context = context;
            Medicamentos = new ObservableCollection<Medicamento>();
            MedicamentosCaducados = new ObservableCollection<Medicamento>();
            MedicamentosCollectionView.ItemsSource = Medicamentos;
            MedicamentosCaducadosCollectionView.ItemsSource = MedicamentosCaducados;

            EditarCommand = new Command<Medicamento>(OnEditarMedicamento);
            EliminarCommand = new Command<Medicamento>(OnEliminarMedicamento);

            CargarMedicamentos();
            BindingContext = this;
            _isMenuOpen = false;
            NavigationPage.SetHasBackButton(this, false);
        }

        private async void CargarMedicamentos()
        {
            var medicamentos = await _context.Medicamentos.ToListAsync();
            Medicamentos.Clear();
            MedicamentosCaducados.Clear();

            foreach (var medicamento in medicamentos)
            {
                Medicamentos.Add(medicamento);

                // Agregar a la lista de caducados si la fecha es menor o igual a 30 días desde hoy
                if (medicamento.FechaCaducidad <= DateTime.Now.AddDays(30))
                {
                    MedicamentosCaducados.Add(medicamento);
                }
            }
        }

        public void ActualizarListaMedicamentos(Medicamento medicamento)
        {
            var existente = Medicamentos.FirstOrDefault(m => m.Id == medicamento.Id);
            if (existente != null)
            {
                Medicamentos.Remove(existente);
            }
            Medicamentos.Add(medicamento);

            // Actualizar la lista de caducados
            if (medicamento.FechaCaducidad <= DateTime.Now.AddDays(30))
            {
                if (!MedicamentosCaducados.Contains(medicamento))
                {
                    MedicamentosCaducados.Add(medicamento);
                }
            }
            else
            {
                MedicamentosCaducados.Remove(medicamento);
            }
        }

        public async void OnEditarMedicamento(Medicamento medicamento)
        {
            await Navigation.PushAsync(new RegistrarMedicamento(_context, medicamento, this));
        }

        public async void OnEliminarMedicamento(Medicamento medicamento)
        {
            bool confirmacion = await DisplayAlert("Confirmar", $"¿Desea eliminar el medicamento {medicamento.Nombre}?", "Sí", "No");
            if (confirmacion)
            {
                _context.Medicamentos.Remove(medicamento);
                await _context.SaveChangesAsync();
                Medicamentos.Remove(medicamento);
                MedicamentosCaducados.Remove(medicamento);
                await DisplayAlert("Éxito", "Medicamento eliminado correctamente.", "OK");
            }
        }

        private async void OnAgregarMedicamentoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrarMedicamento(_context, null, this));
        }

        private async void OnBuscarMedicamento(object sender, TextChangedEventArgs e)
        {
            string textoBusqueda = e.NewTextValue.ToLower();
            Medicamentos.Clear();

            var medicamentos = await _context.Medicamentos.ToListAsync();
            foreach (var medicamento in medicamentos)
            {
                if (medicamento.Nombre.ToLower().Contains(textoBusqueda))
                {
                    Medicamentos.Add(medicamento);
                }
            }
        }

        private void OnOpenMenuClicked(object sender, EventArgs e)
        {
            FlyoutMenu.IsVisible = !FlyoutMenu.IsVisible;
            _isMenuOpen = FlyoutMenu.IsVisible;
            MenuButton.Source = _isMenuOpen ? "logon.png" : "close.png";
        }

        private async void OnUserpage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CRUDAdmin(_context));
        }

        private async void OnCalendarpage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new adminView(_context));
        }
    }
}
