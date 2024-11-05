using MedicalUTP.ViewModel;
using MedicalUTP.DataAcess;

namespace MedicalUTP.Pages
{
    public partial class HistorialDoctor : ContentPage
    {
        private readonly MedicalUTPDbContext _context;
        private readonly HistorialDoctorViewModel _viewModel;

        public HistorialDoctor(HistorialDoctorViewModel viewModel, MedicalUTPDbContext context)
        {
            InitializeComponent();
            _context = context;
            _viewModel = viewModel;
            BindingContext = viewModel;
        }
    }
}
