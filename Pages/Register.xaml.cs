using MedicalUTP.DataAcess;
using MedicalUTP.Models;
using System;

namespace MedicalUTP.Pages
{
    public partial class Register : ContentPage
    {
        private readonly MedicalUTPDbContext _context;

        public Register(MedicalUTPDbContext context)
        {
            InitializeComponent();
            _context = context;

            // Si no configuras los �tems en XAML, puedes hacerlo aqu�:
            // RolPicker.Items.Add("Admin");
            // RolPicker.Items.Add("Estudiante");
            // RolPicker.Items.Add("Docente");
            // RolPicker.Items.Add("Empleado");
        }

        private async void OnSaveUserClicked(object sender, EventArgs e)
        {
            // Verificar si todos los campos est�n completos
            if (string.IsNullOrWhiteSpace(UsuarioEntry.Text) ||
                string.IsNullOrWhiteSpace(TelefonoEntry.Text) ||
                string.IsNullOrWhiteSpace(CedulaEntry.Text) ||
                string.IsNullOrWhiteSpace(EmailEntry.Text) ||
                string.IsNullOrWhiteSpace(PasswordEntry.Text))
            {
                await DisplayAlert("Error", "Por favor, completa todos los campos.", "OK");
                return;
            }

            // Verificar si un rol ha sido seleccionado
            if (RolPicker.SelectedItem == null)
            {
                await DisplayAlert("Error", "Por favor, selecciona un rol.", "OK");
                return;
            }

            // Validar formato del correo electr�nico (opcional)
            if (!IsValidEmail(EmailEntry.Text))
            {
                await DisplayAlert("Error", "Por favor, ingresa un correo electr�nico v�lido.", "OK");
                return;
            }

            // Obtener el rol seleccionado
            string selectedRole = RolPicker.SelectedItem as string ?? "DefaultRole";

            // Crear un nuevo usuario
            var user = new User
            {
                Nombre = UsuarioEntry.Text,
                Telefono = TelefonoEntry.Text,
                Cedula = CedulaEntry.Text,
                Correo = EmailEntry.Text,
                Password = PasswordEntry.Text,
                Role = selectedRole
            };

            // Guardar el usuario en la base de datos
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            // Mostrar mensaje de �xito
            await DisplayAlert("Success", "�Usuario guardado exitosamente!", "OK");

            // Redirigir al usuario a la p�gina de inicio de sesi�n
            await Navigation.PopAsync();
        }

        // M�todo para validar el formato del correo electr�nico
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Navegar a la p�gina de inicio de sesi�n
        public async void IrASingIn(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
