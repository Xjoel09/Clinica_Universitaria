namespace MedicalUTP.Pages;
using MedicalUTP.DataAcess;
using MedicalUTP.Models;
using MedicalUTP.ViewModel;
using System;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

    public partial class Inventario : ContentPage
    {
        private readonly MedicalUTPDbContext _context;
        public ObservableCollection<Medicamento> Medicamentos { get; set; }

        public Inventario(MedicalUTPDbContext context)
        {
            InitializeComponent();
            _context = context;
            Medicamentos = new ObservableCollection<Medicamento>();
            MedicamentosCollectionView.ItemsSource = Medicamentos;
            CargarMedicamentos();
        }

    public Inventario()
    {
       
        
    }

    private async void CargarMedicamentos()
        {
            var medicamentos = await _context.Medicamentos.ToListAsync();
            foreach (var medicamento in medicamentos)
            {
                Medicamentos.Add(medicamento);
            }
        }

        private async void OnAgregarMedicamentoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrarMedicamento(_context));
        }

        private void OnBuscarMedicamento(object sender, EventArgs e)
        {
            // lógica de búsqueda
        }
    }
