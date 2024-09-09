using MedicalUTP.DataAcess;
using MedicalUTP.ViewsModel;

namespace MedicalUTP.Pages
{
    public partial class Login : ContentPage
    {
        private readonly MedicalUTPDbContext _context;

        public Login(MedicalUTPDbContext context, LoginViewModel viewModel)
        {
            _context = context;
            InitializeComponent();
            BindingContext = viewModel;
        }

        public async void IrASingUn(object sender, EventArgs e)
        {
            //DbContext a la página de registro
            await Navigation.PushAsync(new Register(_context));
        }
    }
}
