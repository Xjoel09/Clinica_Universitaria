using MedicalUTP.DataAcess;
using MedicalUTP.Models;
using MedicalUTP.ViewsModel;

namespace MedicalUTP.Pages;

public partial class Inventario : ContentPage
{
    public Inventario(MedicalUTPDbContext context)
    {
        InitializeComponent();
        BindingContext = new InventarioViewModel(context);
    }

    private void MedicamentosListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var viewModel = (InventarioViewModel)BindingContext;
        viewModel.SelectedMedicamento = e.SelectedItem as MedicalUTP.Models.Inventario;
    }

    private async void MostrarFormularioAgregarMedicamento_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddInventory((MedicalUTPDbContext)BindingContext));
    }
}