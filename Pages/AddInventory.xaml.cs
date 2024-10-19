using MedicalUTP.DataAcess;
namespace MedicalUTP.Pages;

public partial class AddInventory : ContentPage
{
    private readonly MedicalUTPDbContext _context;
    public AddInventory(MedicalUTPDbContext context)
	{
		InitializeComponent();
        _context = context;
    }

	private async void Guardar_Clicked(object sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(NombreEntry.Text) || 
			string.IsNullOrWhiteSpace(DescripcionEntry.Text) || 
			!int.TryParse(CantidadEntry.Text, out int cantidad) || 
			!int.TryParse(PrecioEntry.Text, out int precio))
		{
			await DisplayAlert("Error", "Por favor, complete todos los campos correctamente.", "OK");
			return;
		}

		var nuevoMedicamento = new Models.Inventario
        {
			Nombre = NombreEntry.Text,
			Descripcion = DescripcionEntry.Text,
			Cantidad = cantidad,
			Precio = precio
		};

		try
		{
            _context.Inventario.Add(nuevoMedicamento);
				await _context.SaveChangesAsync();
			
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", "Ocurrió un error al guardar el medicamento: " + ex.Message, "OK");
			return;
		}

		await Navigation.PopAsync();
	}
}
