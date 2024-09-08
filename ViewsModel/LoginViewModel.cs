using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using MedicalUTP.DataAcess;
using MedicalUTP.Pages;
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
        }
        public LoginViewModel()
        {

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
                    Application.Current.MainPage = new CRUDAdmin();
                }
                else if (user.Role == "Estudiante")
                {
                    //isAdmin = false;
                    //isStudent = true;

                    var MainPage = Application.Current?.MainPage ?? throw new InvalidOperationException("No se pudo acceder a MainPage.");
                    await MainPage.DisplayAlert("Mensaje", "Bienvenido Estudiante", "Aceptar");
                    Application.Current.MainPage = new FlyoutMenu();
                }
                else if (user.Role == "Docente")
                {
                    var MainPage = Application.Current?.MainPage ?? throw new InvalidOperationException("No se pudo acceder a MainPage.");
                    await MainPage.DisplayAlert("Mensaje", "Bienvenido Docente", "Aceptar");
                    Application.Current.MainPage = new FlyoutMenu();
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
