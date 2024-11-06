using MedicalUTP.DataAcess;
using MedicalUTP.Models;
using System;
using Microsoft.Maui.Controls;

namespace MedicalUTP.Pages
{
    public partial class RegistrarMedicamento : ContentPage
    {
        private readonly MedicalUTPDbContext _context;
        private Medicamento _medicamento;
        private Inventario _inventarioPage;

        // Constructor para creaci�n de un nuevo medicamento
        public RegistrarMedicamento(MedicalUTPDbContext context, Medicamento medicamento = null, Inventario inventarioPage = null)
        {
            InitializeComponent();
            _context = context;
            _medicamento = medicamento;
            _inventarioPage = inventarioPage;

            // Si estamos en modo edici�n, rellenamos los campos con los datos del medicamento
            if (_medicamento != null)
            {
                NombreEntry.Text = _medicamento.Nombre;
                CantidadEntry.Text = _medicamento.Cantidad.ToString();
                PrecioEntry.Text = _medicamento.Precio.ToString();
                FechaEntregaPicker.Date = _medicamento.FechaEntrega;
                FechaCaducidadPicker.Date = _medicamento.FechaCaducidad;
            }
        }

        private async void OnGuardarMedicamentoClicked(object sender, EventArgs e)
        {
            try
            {
                // Si no existe un medicamento, estamos en modo creaci�n
                if (_medicamento == null)
                {
                    _medicamento = new Medicamento
                    {
                        Nombre = NombreEntry.Text,
                        Cantidad = int.Parse(CantidadEntry.Text),
                        Precio = decimal.Parse(PrecioEntry.Text),
                        FechaEntrega = FechaEntregaPicker.Date,
                        FechaCaducidad = FechaCaducidadPicker.Date
                    };
                    _context.Medicamentos.Add(_medicamento);

                    // Agrega el nuevo medicamento a la lista en Inventario
                    _inventarioPage?.ActualizarListaMedicamentos(_medicamento);
                }
                else
                {
                    // Si ya existe, actualizamos los datos
                    _medicamento.Nombre = NombreEntry.Text;
                    _medicamento.Cantidad = int.Parse(CantidadEntry.Text);
                    _medicamento.Precio = decimal.Parse(PrecioEntry.Text);
                    _medicamento.FechaEntrega = FechaEntregaPicker.Date;
                    _medicamento.FechaCaducidad = FechaCaducidadPicker.Date;
                    _context.Medicamentos.Update(_medicamento);

                    // Actualizamos la lista en Inventario
                    _inventarioPage?.ActualizarListaMedicamentos(_medicamento);
                }

                // Guardamos los cambios en la base de datos
                await _context.SaveChangesAsync();
                await DisplayAlert("�xito", "Medicamento guardado exitosamente!", "OK");

                // Volvemos a la p�gina de inventario
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurri� un error al guardar el medicamento: {ex.Message}", "OK");
            }
        }
    }
}



