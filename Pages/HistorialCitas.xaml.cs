using MedicalUTP.ViewsModel;
using MedicalUTP.DataAcess;
namespace MedicalUTP.Pages;

public partial class HistorialCitas : ContentPage
{
    private readonly MedicalUTPDbContext _context;
    private readonly HistorialCitasViewModel _viewModel;
    public HistorialCitas(HistorialCitasViewModel viewModel, MedicalUTPDbContext context)
    {
        InitializeComponent();
        _context = context;
        _viewModel = viewModel;
        BindingContext = viewModel;
    }
}