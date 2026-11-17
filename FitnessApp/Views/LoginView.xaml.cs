using System.Windows;
using System.Windows.Controls;
using FitnessApp.ViewModels;

namespace FitnessApp.Views
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        // Обработчик для поля пароля ВХОДА
        private void LoginPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is AuthViewModel vm)
            {
                vm.Password = ((PasswordBox)sender).Password;
            }
        }

        // Обработчик для поля пароля РЕГИСТРАЦИИ
        private void RegPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is AuthViewModel vm)
            {
                vm.RegPassword = ((PasswordBox)sender).Password;
            }
        }
    }
}