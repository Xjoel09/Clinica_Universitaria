namespace MedicalUTP.Pages;
using System;
using MedicalUTP.ViewModel;
public partial class Solicitud : ContentPage
{
    public Solicitud()
    {
        InitializeComponent();

        // Aqu� asignamos el ViewModel a la p�gina
        BindingContext = new ConsultaViewModel();
    }
}