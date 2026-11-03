using Microsoft.EntityFrameworkCore;
using FitnessApp.Models;

namespace FitnessApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<WeekPlan> WeekPlans { get; set; }
        public DbSet<DayPlan> DayPlans { get; set; }
        public DbSet<WorkoutSession> WorkoutSessions { get; set; }
        public DbSet<SetLog> SetLogs { get; set; }
        public DbSet<MuscleGroup> MuscleGroups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Используем MS SQL Server LocalDB. 
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=FitnessAppDb;Integrated Security=True;TrustServerCertificate=True");
        }
    }
}