using MedicalUTP.DataAcess;
using MedicalUTP.Pages;
using MedicalUTP.ViewModel;
using MedicalUTP.ViewsModel;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalUTP
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; }

        public App()
        {
            InitializeComponent();

            var services = new ServiceCollection();
            ConfigureServices(services);

            Services = services.BuildServiceProvider();

            // Inyectar base de datos y el ViewModel en la página de Login
            var context = Services.GetRequiredService<MedicalUTPDbContext>();
            var loginViewModel = Services.GetRequiredService<LoginViewModel>();

            MainPage = new NavigationPage(new Login(context, loginViewModel));
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Registrar base de datos y el ViewModel como servicios
            services.AddSingleton<MedicalUTPDbContext>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<ConsultaViewModel>();
            services.AddTransient<HistorialCitasViewModel>();
            services.AddTransient<HistorialCitas>();
            services.AddTransient<Solicitud>();
            services.AddTransient<Servicios>();
            services.AddTransient<Consultas>(); 

        }

        // Método para cambiar a FlyoutMenu después del login, optimiza el codigo de el LoginViewModel
        public static void GoToMainPage()
        {
            var context = Services.GetRequiredService<MedicalUTPDbContext>();
            MainThread.BeginInvokeOnMainThread(() =>
            {
                 var MainPage = Application.Current?.MainPage ?? throw new InvalidOperationException("No se pudo acceder a MainPage.");
                 MainPage.DisplayAlert("Mensaje", "Bienvenido Estudiante", "Aceptar");
                 Application.Current.MainPage = new FlyoutMenu(context, Services);
            });
        }
    }
}

