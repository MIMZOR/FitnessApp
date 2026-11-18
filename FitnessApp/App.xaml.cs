using System.Windows;
using FitnessApp.Data;

namespace FitnessApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();
            }
        }
    }
}