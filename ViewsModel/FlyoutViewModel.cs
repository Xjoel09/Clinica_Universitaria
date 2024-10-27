using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalUTP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls;
using MedicalUTP.DataAcess;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using MedicalUTP.ViewsModel;


/*namespace MedicalUTP.ViewModels
{
  
    public partial class FlyoutViewModel : ObservableObject
    {
        private readonly MedicalUTPDbContext _context;
        [ObservableProperty]
        private ObservableCollection<FlyoutItems> menuItems; // Lower camel case to avoid conflicts

        public FlyoutViewModel(MedicalUTPDbContext context)
        {
            LoadMenuItems();
            _context = context;
        }

        private void LoadMenuItems()
        {
            // Simula el rol del usuario para cargar los ítems del menú
            var userRole = Preferences.Get("userRole", "Estudiante");
            var userRole1 = Preferences.Get("userRole", "Doctor");
            menuItems = new ObservableCollection<FlyoutItems>();

            // Agregar items según el rol
            if (userRole == "Estudiante")
            {
                menuItems.Add(new FlyoutItems { Title = "Inicio", IconSource = "casa.svg" });
                menuItems.Add(new FlyoutItems { Title = "Solicitudes", IconSource = "solicitudes.svg" });
            }
            else if (userRole == "Doctor")
            {
                menuItems.Add(new FlyoutItems { Title = "Historial", IconSource = "historial.svg" });
                menuItems.Add(new FlyoutItems { Title = "Consultas", IconSource = "flecha1.svg" });
            }
            else if (userRole == "Admin")
            {
                menuItems.Add(new FlyoutItems { Title = "Administración", IconSource = "admin.svg" });
            }
        }
    }


}

*/
