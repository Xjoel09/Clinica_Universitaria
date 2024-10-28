//using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using MedicalUTP.DataAcess;
using MedicalUTP.ViewsModel;
namespace MedicalUTP.Pages;

public partial class adminView : ContentPage
{
    private bool _isMenuOpen;
    private readonly MedicalUTPDbContext _context;
    public adminView(MedicalUTPDbContext context)
    {
        InitializeComponent();
        _context = context;
        BindingContext = new CRUDAdminViewModel(_context);
        _isMenuOpen = false;  // Inicia el menú como cerrado

        NavigationPage.SetHasBackButton(this, false);
    }

    public adminView()
    {
    }

    // Evento para abrir el Flyout
    private void OnOpenMenuClicked(object sender, EventArgs e)
    {
        // Cambia el estado de visibilidad del menú
        FlyoutMenu.IsVisible = !FlyoutMenu.IsVisible;

        // Cambia la imagen del botón según el estado del menú
        _isMenuOpen = FlyoutMenu.IsVisible;
        MenuButton.Source = _isMenuOpen ? "logon.png" : "close.png";
    }

    private async void OnUserpage_Clicked(object sender, EventArgs e)
    {
        var context = new MedicalUTPDbContext(); // o obtiene el contexto desde el servicio de dependencia
        await Navigation.PushAsync(new CRUDAdmin(context));
    }

    private async void OnPillspage_Clicked(object sender, EventArgs e)
    {
        var context = new MedicalUTPDbContext(); // o obtener el contexto desde el servicio de dependencia
        await Navigation.PushAsync(new Inventario(context)); // Agrega la nueva página
    }
}