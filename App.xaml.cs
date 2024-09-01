using MedicalUTP.Pages;

namespace MedicalUTP
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new FlyoutMenu();
        }
    }
}
