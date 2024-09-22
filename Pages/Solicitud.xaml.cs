namespace MedicalUTP.Pages;
using System;
using MedicalUTP.ViewModel;
using MedicalUTP.DataAcess;
using SQLitePCL;

public partial class Solicitud : ContentPage
{
    private readonly MedicalUTPDbContext _context;
    private readonly ConsultaViewModel _viewModel;
    public Solicitud(ConsultaViewModel viewModel, MedicalUTPDbContext context)
    {
        InitializeComponent();
        _context = context;
        _viewModel = viewModel;
        BindingContext = viewModel;
    }
}