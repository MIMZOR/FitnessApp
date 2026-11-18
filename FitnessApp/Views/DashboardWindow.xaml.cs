using System.Windows;
using System.Windows.Controls;
using FitnessApp.Views;

namespace FitnessApp.Views
{
    public partial class DashboardWindow : Window
    {
        public DashboardWindow()
        {
            InitializeComponent();
            // При запуске сразу открываем Главную
            PageTitle.Text = "Главная";
            MainContent.Content = new HomeView();
        }

        // Обработчик для кнопки "Главная"
        private void Nav_Click(object sender, RoutedEventArgs e)
        {
            PageTitle.Text = "Главная";
            MainContent.Content = new HomeView();
        }

        private void StartWorkout_Click(object sender, RoutedEventArgs e)
        {
            PageTitle.Text = "Тренировка";
            MessageBox.Show("Тренировка в разработке");
        }

        private void Schedule_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is FitnessApp.Models.User user)
            {
                PageTitle.Text = "Моё расписание";
                MainContent.Content = new ScheduleView(user.user_id);
            }
        }

        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            PageTitle.Text = "Статистика";
            MessageBox.Show("Статистика в разработке");
        }

        private void Exercises_Click(object sender, RoutedEventArgs e)
        {
            // ИСПРАВЛЕНИЕ: Меняем заголовок при переходе
            PageTitle.Text = "Справочник упражнений";
            MainContent.Content = new ExercisesView();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var loginView = new LoginView();
            loginView.Show();
            this.Close();
        }
    }
}