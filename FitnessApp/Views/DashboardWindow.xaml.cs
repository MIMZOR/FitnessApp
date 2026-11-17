using System.Windows;
using FitnessApp.Views;

namespace FitnessApp.Views
{
    public partial class DashboardWindow : Window
    {
        public DashboardWindow()
        {
            InitializeComponent();
        }

        private void StartWorkout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Здесь будет открываться окно записи подходов (WorkoutView).", "В разработке");
        }

        private void Schedule_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Здесь будет календарь с планированием недель (WeekPlan).", "В разработке");
        }

        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Здесь будет выводиться статистика тренировок.", "В разработке");
        }

        private void Exercises_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Здесь будет справочник упражнений по группам мышц.", "В разработке");
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            // Возвращаемся на экран авторизации
            var loginView = new LoginView();
            loginView.Show();
            this.Close();
        }
    }
}