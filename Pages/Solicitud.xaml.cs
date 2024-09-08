namespace MedicalUTP.Pages;
using System;
using MedicalUTP.ViewModel;
public partial class Solicitud : ContentPage
{
    public Solicitud()
    {
        InitializeComponent();

        // Aquí asignamos el ViewModel a la página
        BindingContext = new ConsultaViewModel();
    }
}