using FitnessApp.ViewModels;
using System.Windows.Controls;

namespace FitnessApp.Views
{
    public partial class ScheduleView : UserControl
    {
        // Конструктор по умолчанию (нужен для XAML-дизайнера)
        public ScheduleView()
        {
            InitializeComponent();
        }

        // Новый конструктор с передачей ID
        public ScheduleView(int userId)
        {
            InitializeComponent();
            DataContext = new ScheduleViewModel(userId);
        }
    }
}