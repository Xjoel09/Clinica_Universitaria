namespace MedicalUTP.Pages;
using MedicalUTP.DataAcess;
using MedicalUTP.Models;
using System;
public partial class RegistrarMedicamento : ContentPage
{
        private readonly MedicalUTPDbContext _context;

    public RegistrarMedicamento()
    {
    }

    public RegistrarMedicamento(MedicalUTPDbContext context)
        {
            InitializeComponent();
            _context = context;
        }

    private async void OnGuardarMedicamentoClicked(object sender, EventArgs e)
    {
        var medicamento = new Medicamento
        {
            Nombre = NombreEntry.Text,
            Cantidad = int.Parse(CantidadEntry.Text),
            Precio = decimal.Parse(PrecioEntry.Text)
        };

        _context.Medicamentos.Add(medicamento);
        await _context.SaveChangesAsync();

        await DisplayAlert("Éxito", "Medicamento guardado exitosamente!", "OK");
        await Navigation.PopAsync();
    }
}
