namespace MedicalUTP.Pages;
using MedicalUTP.ViewsModel;
using MedicalUTP.DataAcess;
using MedicalUTP.ViewModel;

public partial class FlyoutMenu : FlyoutPage
{
    private readonly MedicalUTPDbContext _context;
    private readonly IServiceProvider _serviceProvider;
    public FlyoutMenu(MedicalUTPDbContext context, IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _context = context;
        _serviceProvider = serviceProvider;

        flyoutPage.collectioView.SelectionChanged += CollectionView_SelectionChanged;
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var item = e.CurrentSelection.FirstOrDefault() as FlyoutItems;

        if (item != null)
        {
            if (item.Title == "Cerrar Sesión")
            {
                Preferences.Remove("logueado");

               
                Application.Current.MainPage = new NavigationPage(new Login(_context, new LoginViewModel(_context)))
                {
                    BarBackgroundColor = Colors.White
                };
            }
            else
            {
               
                var page = ResolvePage(item.Title);
                if (page != null)
                {
                    Detail = new NavigationPage(page);

                    if (DeviceInfo.Platform == DevicePlatform.iOS || DeviceInfo.Platform == DevicePlatform.Android)
                    {
                        IsPresented = false;
                    }
                    else
                    {
                        IsPresented = true;
                    }
                }
            }
        }
    }

    private Page ResolvePage(string title)
    {
        switch (title)
        {
            case "Inicio":
                return new Home();//_serviceProvider.GetService<Home>();

            case "Servicios":
                return new Servicios();//_serviceProvider.GetService<Servicios>();no encontre la manera de usar esto
              //return  Application.Current.MainPage = new NavigationPage(new Servicios()); esto elimina el menu

            case "Solicitudes":
               // return _serviceProvider.GetService<Solicitud>();
                var consultaViewModel = new ConsultaViewModel(_context);
                return new Solicitud(consultaViewModel, _context);
            case "Historial":
                //return _serviceProvider.GetService<HistorialCitas>();
                var historialCitasViewModel = new HistorialCitasViewModel(_context);
                return new HistorialCitas(historialCitasViewModel, _context);
            case "Consultas":
                return  new Consultas();// _serviceProvider.GetService<Consultas>();
            case "Contacto":
                return new Contactos();//_serviceProvider.GetService<Contactos>();

            case "Inventario":
                return new Inventario(_context);

            case "RegistrarMedicamento":
                return new RegistrarMedicamento(_context);

            case "HistorialDoctor":
                //return _serviceProvider.GetService<HistorialDoctor>();
                var historialDoctorViewModel = new HistorialDoctorViewModel(_context);
                return new HistorialDoctor(historialDoctorViewModel, _context);

            default:
                Console.WriteLine($"No se pudo resolver la página para el título: {title}");
                return null;
        }
    }
}


    