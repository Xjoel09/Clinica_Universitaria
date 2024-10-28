namespace MedicalUTP.Pages;
using MedicalUTP.DataAcess;
using MedicalUTP.Models;
using MedicalUTP.ViewModel;
using System;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using MedicalUTP.ViewsModel;


    public partial class Inventario : ContentPage
    {
        private readonly MedicalUTPDbContext _context;
        public ObservableCollection<Medicamento> Medicamentos{ get; set; }
        private bool _isMenuOpen;
    public Inventario(MedicalUTPDbContext context)
        {
            InitializeComponent();
            _context = context;
            Medicamentos= new ObservableCollection<Medicamento>();
        MedicamentosCollectionView.ItemsSource = Medicamentos;
            CargarMedicamentos();
            BindingContext = new CRUDAdminViewModel(_context);
            _isMenuOpen = false;  // Inicia el menú como cerrado
            NavigationPage.SetHasBackButton(this, false);
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
        // Evento para abrir el Flyout
        private void OnOpenMenuClicked(object sender, EventArgs e)
        {
            // Cambia el estado de visibilidad del menú
            FlyoutMenu.IsVisible = !FlyoutMenu.IsVisible;

            // Cambia la imagen del botón según el estado del menú
            _isMenuOpen = FlyoutMenu.IsVisible;
            MenuButton.Source = _isMenuOpen ? "logon.png" : "close.png";
        }

        private async void OnUserpage_Clicked(object sender, EventArgs e)
        {
            var context = new MedicalUTPDbContext(); // o obtiene el contexto desde el servicio de dependencia
            await Navigation.PushAsync(new CRUDAdmin(context));
        }

        private async void OnCalendarpage_Clicked(object sender, EventArgs e)
        {
            var context = new MedicalUTPDbContext(); // o obtiene el contexto desde el servicio de dependencia
            await Navigation.PushAsync(new adminView(context));
        }
}
