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
        }

        private async void OnSaveUserClicked(object sender, EventArgs e)
        {
            // Verificar si los campos están completos
            if (string.IsNullOrWhiteSpace(UsuarioEntry.Text) ||
                string.IsNullOrWhiteSpace(TelefonoEntry.Text) ||
                string.IsNullOrWhiteSpace(CedulaEntry.Text) ||
                string.IsNullOrWhiteSpace(EmailEntry.Text) ||
                string.IsNullOrWhiteSpace(PasswordEntry.Text))
            {
                await DisplayAlert("Error", "Por favor, completa todos los campos.", "OK");
                return;
            }

            if (RolPicker.SelectedItem == null)
            {
                await DisplayAlert("Error", "Por favor, selecciona un rol.", "OK");
                return;
            }

            if (!IsValidEmail(EmailEntry.Text))
            {
                await DisplayAlert("Error", "Por favor, ingresa un correo electrónico válido.", "OK");
                return;
            }

            string selectedRole = RolPicker.SelectedItem as string ?? "DefaultRole";

            var user = new User
            {
                Nombre = UsuarioEntry.Text,
                Telefono = TelefonoEntry.Text,
                Cedula = CedulaEntry.Text,
                Correo = EmailEntry.Text,
                Password = PasswordEntry.Text,
                Role = selectedRole
            };

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            await DisplayAlert("Success", "¡Usuario guardado exitosamente!", "OK");

            await Navigation.PopAsync();
        }

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
        public async void IrASingIn(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
