using MedicalUTP.DataAcess;
using MedicalUTP.Pages;

namespace MedicalUTP
{
    public partial class App : Application
    {
        private readonly MedicalUTPDbContext _context;
        public App(MedicalUTPDbContext context)
        {
            InitializeComponent();
            _context = context;
            MainPage = new NavigationPage( new Login(_context));
        }
    }
}
