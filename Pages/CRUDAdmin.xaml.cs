//using AndroidX.Lifecycle;
using MedicalUTP.DataAcess;
using MedicalUTP.ViewsModel;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace MedicalUTP.Pages;

public partial class CRUDAdmin : ContentPage
{
    private readonly MedicalUTPDbContext _context;

    private bool _isMenuOpen;
    public CRUDAdmin(MedicalUTPDbContext context)
    {
        InitializeComponent();
        _context = context;
        BindingContext = new CRUDAdminViewModel(_context);
        _isMenuOpen = false;  // Inicia el menú como cerrado

        NavigationPage.SetHasBackButton(this, false);


    }

    public CRUDAdmin()
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


    private async void OnCalendarpage_Clicked(object sender, EventArgs e)
    {
        var context = new MedicalUTPDbContext(); // o obtener el contexto desde el servicio de dependencia
        await Navigation.PopAsync(); // Elimina la página actual
        await Navigation.PushAsync(new adminView(context)); // Agrega la nueva página
    }

    private async void OnPillspage_Clicked(object sender, EventArgs e)
    {
        var context = new MedicalUTPDbContext(); // o obtener el contexto desde el servicio de dependencia
        await Navigation.PopAsync(); // Elimina la página actual
        await Navigation.PushAsync(new Inventario(context)); // Agrega la nueva página
    }

}