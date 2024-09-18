using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MedicalUTP.DataAcess;
using MedicalUTP.Pages;
using MedicalUTP.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalUTP.ViewsModel
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly MedicalUTPDbContext _context;

        [ObservableProperty]
        public string usuario = string.Empty;
        [ObservableProperty]
        public string password = string.Empty;
        [ObservableProperty]
        public bool isAdmin;

        [ObservableProperty]
        public bool isStudent;


        public LoginViewModel(MedicalUTPDbContext context)
        {
            _context = context;
            EnsureAdminAccountExists();
        }
        public LoginViewModel()
        {

        }

        private async Task EnsureAdminAccountExists()
        {
            var adminExists = await _context.User.AnyAsync(u => u.Role == "Admin");
            if (!adminExists)
            {
                var adminUser = new User
                {
                    Nombre = "Admin",
                    Telefono = "1234567890",
                    Cedula = "admin123",
                    Correo = "admin@example.com",
                    Password = "adminPassword123", 
                    Role = "Admin"
                };

                _context.User.Add(adminUser);
                await _context.SaveChangesAsync();
            }
        }

        [RelayCommand]
        private async Task Login()
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Nombre == Usuario && u.Password == Password);

            if (user != null)
            {
                Preferences.Set("logueado", "si");


                if (user.Role == "Admin")
                {
                    // isAdmin = true;
                    // isStudent = false;



                    var MainPage = Application.Current?.MainPage ?? throw new InvalidOperationException("No se pudo acceder a MainPage.");
                    await MainPage.DisplayAlert("Mensaje", "Bienvenido Administrador", "Aceptar");
                    Application.Current.MainPage = new CRUDAdmin(_context);
                }
                else if (user.Role == "Estudiante")
                {
                    //isAdmin = false;
                    //isStudent = true;

                    var MainPage = Application.Current?.MainPage ?? throw new InvalidOperationException("No se pudo acceder a MainPage.");
                    await MainPage.DisplayAlert("Mensaje", "Bienvenido Estudiante", "Aceptar");
                    Application.Current.MainPage = new FlyoutMenu(_context);
                }
                else if (user.Role == "Docente")
                {
                    var MainPage = Application.Current?.MainPage ?? throw new InvalidOperationException("No se pudo acceder a MainPage.");
                    await MainPage.DisplayAlert("Mensaje", "Bienvenido Docente", "Aceptar");
                    Application.Current.MainPage = new FlyoutMenu(_context);
                }
            }
            else
            {
                var MainPage = Application.Current?.MainPage ?? throw new InvalidOperationException("No se pudo acceder a MainPage.");
                await MainPage.DisplayAlert("Mensaje", "No se encontraron coincidencias", "Aceptar");
            }
        }
    }
}
