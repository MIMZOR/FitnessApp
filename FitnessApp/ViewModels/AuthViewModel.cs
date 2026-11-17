using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessApp.Data;
using FitnessApp.Models;
using FitnessApp.Repositories;
using FitnessApp.Views;
using System.Windows;

namespace FitnessApp.ViewModels
{
    public partial class AuthViewModel : ObservableObject
    {
        private readonly UserRepository _repository;

        [ObservableProperty] private string _login = string.Empty;
        [ObservableProperty] private string _password = string.Empty;
        [ObservableProperty] private string _firstName = string.Empty;
        [ObservableProperty] private string _regPassword = string.Empty;

        public AuthViewModel()
        {
            _repository = new UserRepository(new AppDbContext());
        }

        [RelayCommand]
        private void LoginAction()
        {
            var user = _repository.Authenticate(Login, Password);
            if (user != null)
            {
                var dashboard = new DashboardWindow();
                // Передаем данные вошедшего пользователя в окно
                dashboard.DataContext = user;

                dashboard.Show();

                // Закрываем текущее окно входа
                Application.Current.Windows[0]?.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка");
            }
        }

        [RelayCommand]
        private void RegisterAction()
        {
            var newUser = new User
            {
                login = Login,
                first_name = FirstName
            };

            if (_repository.Register(newUser, RegPassword))
            {
                MessageBox.Show("Успешно зарегистрирован! Теперь войдите.", "Успех");
            }
            else
            {
                MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка");
            }
        }
    }
}