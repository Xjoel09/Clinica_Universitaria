namespace MedicalUTP.Pages;
using MedicalUTP.ViewsModel;
using MedicalUTP.DataAcess;

public partial class FlyoutMenu : FlyoutPage
{
    private readonly MedicalUTPDbContext _context;

    // Constructor que recibe el contexto de la base de datos
    public FlyoutMenu(MedicalUTPDbContext context)
    {
        InitializeComponent();
        _context = context; // Asigna el contexto
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

                // Aquí estás pasando el contexto al Login y su ViewModel
                Application.Current.MainPage = new NavigationPage(new Login(_context, new LoginViewModel(_context)))
                {
                    BarBackgroundColor = Colors.White
                };
            }
            else
            {
                // Navegación normal
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                IsPresented = false;
            }
        }
    }
}