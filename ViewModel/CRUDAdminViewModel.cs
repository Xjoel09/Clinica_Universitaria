using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MedicalUTP.DataAcess;
using MedicalUTP.Models;
using MedicalUTP.Pages;
using Microsoft.EntityFrameworkCore;

namespace MedicalUTP.ViewsModel
{
    public partial class CRUDAdminViewModel : ObservableObject
    {
        private readonly MedicalUTPDbContext _context;

        [ObservableProperty]
        private ObservableCollection<User> _users;

        [ObservableProperty]
        private User _selectedUser;

        [ObservableProperty]
        private bool _isEditPopupVisible;

        public CRUDAdminViewModel(MedicalUTPDbContext context)
        {
            _context = context;
            LoadUsers();
        }
        [RelayCommand]
        private async Task LoadUsers()
        {
            var userList = await _context.User.Where(u => u.Role != "Admin").ToListAsync();
            Users = new ObservableCollection<User>(userList);
        }

        [RelayCommand]
        private async Task DeleteUser(User user)
        {
            if (user != null && user.Role != "Admin")
            {
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
                Users.Remove(user);
            }
        }

        [RelayCommand]
        private async Task UpdateUser(User user)
        {
            if (user != null && user.Role != "Admin")
            {
                SelectedUser = new User
                {
                    IdUser = user.IdUser,
                    Nombre = user.Nombre,
                    Telefono = user.Telefono,
                    Cedula = user.Cedula,
                    Correo = user.Correo,
                    Password = user.Password,
                    Role = user.Role
                };
                IsEditPopupVisible = true;

            }
        }

        [RelayCommand]
        private async Task SaveUserChanges()
        {
            if (SelectedUser != null && SelectedUser.Role != "Admin")
            {
                var userToUpdate = Users.FirstOrDefault(u => u.IdUser == SelectedUser.IdUser);
                if (userToUpdate != null)
                {
                    userToUpdate.Nombre = SelectedUser.Nombre;
                    userToUpdate.Telefono = SelectedUser.Telefono;
                    userToUpdate.Cedula = SelectedUser.Cedula;
                    userToUpdate.Correo = SelectedUser.Correo;
                    //userToUpdate.Password = SelectedUser.Password;
                    userToUpdate.Role = SelectedUser.Role;

                    _context.User.Update(userToUpdate);
                    await _context.SaveChangesAsync();
                    await LoadUsers();

                    Users = new ObservableCollection<User>(Users);
                }
                IsEditPopupVisible = false;
            }
        }

        [RelayCommand]
        private void CancelEdit()
        {
            IsEditPopupVisible = false;
        }

        [RelayCommand]
        private async Task LogOut()
        {
            Preferences.Remove("logueado");
            Application.Current.MainPage = new NavigationPage(new Login(_context, new LoginViewModel(_context)));
        }
    }
}
