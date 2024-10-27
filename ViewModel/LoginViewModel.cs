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
        private readonly IServiceProvider _serviceProvider;

        [ObservableProperty]
        public string usuario = string.Empty;
        [ObservableProperty]
        public string password = string.Empty;
        [ObservableProperty]
        public bool isAdmin;

        [ObservableProperty]
        public bool isStudent;

        [ObservableProperty]
        public bool isDoctor;


        public LoginViewModel(MedicalUTPDbContext context)
        {
            _context = context;
            EnsureAdminAccountExists();
            EnsureDocAccountExists();
        }
        public LoginViewModel()
        {

        }

        private async Task EnsureDocAccountExists()
        {
            var docExists = await _context.User.AnyAsync(u => u.Role == "Doctor");
            if (!docExists)
            {
                var doc1User = new User
                {
                    Nombre = "Juan Aguilar",
                    Telefono = "65432178",
                    Cedula = "4-334-5567",
                    Correo = "juanes@example.com",
                    Password = "Doc1",
                    Role = "Doctor"
                };
                var doc2User = new User
                {
                    Nombre = "Acuña Chapulini",
                    Telefono = "63322113",
                    Cedula = "4-334-5545",
                    Correo = "gomez@example.com",
                    Password = "Doc2",
                    Role = "Doctor"
                };
                var doc3User = new User
                {
                    Nombre = "Vellonicimo Cordoba",
                    Telefono = "65432132",
                    Cedula = "4-334-5221",
                    Correo = "Ortega@example.com",
                    Password = "Doc3",
                    Role = "Doctor"
                };



                _context.User.Add(doc1User);
                _context.User.Add(doc2User);
                _context.User.Add(doc3User);
                await _context.SaveChangesAsync();
            }
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
                // Después de la autenticación exitosa
                
                if (user.Role == "Admin")
                {
                    // isAdmin = true;
                    // isStudent = false;



                    var MainPage = Application.Current?.MainPage ?? throw new InvalidOperationException("No se pudo acceder a MainPage.");
                    await MainPage.DisplayAlert("Mensaje", "Bienvenido Administrador", "Aceptar");
                    Application.Current.MainPage = new CRUDAdmin( _context);
                }
                else if (user.Role == "Estudiante")
                {
                    //isAdmin = false;
                    //isStudent = true;

                    //var MainPage = Application.Current?.MainPage ?? throw new InvalidOperationException("No se pudo acceder a MainPage.");
                    //await MainPage.DisplayAlert("Mensaje", "Bienvenido Estudiante", "Aceptar");
                    //Application.Current.MainPage = new FlyoutMenu(_context, _serviceProvider);
                    App.GoToMainPage();
                }
                else if (user.Role == "Docente")
                {
                    //var MainPage = Application.Current?.MainPage ?? throw new InvalidOperationException("No se pudo acceder a MainPage.");
                    //await MainPage.DisplayAlert("Mensaje", "Bienvenido Docente", "Aceptar");
                    //Application.Current.MainPage = new FlyoutMenu(_context, _serviceProvider);
                    App.GoToMainPage();
                }
                else if (user.Role == "Doctor")
                {
                    //IsDoctor = true;
                    //IsAdmin = false;
                    //IsStudent = false;

                    //var MainPage = Application.Current?.MainPage ?? throw new InvalidOperationException("No se pudo acceder a MainPage.");
                    //await MainPage.DisplayAlert("Mensaje", "Bienvenido Docente", "Aceptar");
                    //Application.Current.MainPage = new FlyoutMenu(_context, _serviceProvider);
                    App.GoToMainPage();
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
