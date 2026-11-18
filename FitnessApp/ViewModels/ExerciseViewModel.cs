using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessApp.Data;
using FitnessApp.Models;
using System.Windows;
using Microsoft.EntityFrameworkCore; // Убедись, что это есть

namespace FitnessApp.ViewModels
{
    public partial class ExerciseViewModel : ObservableObject
    {
        private readonly AppDbContext _context;

        public ObservableCollection<Exercise> Exercises { get; } = new();
        public ObservableCollection<MuscleGroup> MuscleGroups { get; } = new();

        [ObservableProperty] private string _newExerciseName = string.Empty;
        [ObservableProperty] private string _newExerciseDesc = string.Empty;
        [ObservableProperty] private MuscleGroup? _selectedMuscleGroup;

        public ExerciseViewModel()
        {
            _context = new AppDbContext();
            LoadData();
        }

        private void LoadData()
        {
            var groups = _context.MuscleGroups.ToList();
            if (!groups.Any())
            {
                _context.MuscleGroups.AddRange(
                    new MuscleGroup { name = "Грудь" },
                    new MuscleGroup { name = "Спина" },
                    new MuscleGroup { name = "Ноги" },
                    new MuscleGroup { name = "Руки" }
                );
                _context.SaveChanges();
                groups = _context.MuscleGroups.ToList();
            }

            MuscleGroups.Clear();
            foreach (var g in groups) MuscleGroups.Add(g);

            // ИСПРАВЛЕНИЕ: Теперь мы привязываем сами объекты MuscleGroup, чтобы название отображалось!
            if (!_context.Exercises.Any())
            {
                var chest = groups.FirstOrDefault(g => g.name == "Грудь");
                var back = groups.FirstOrDefault(g => g.name == "Спина");
                var legs = groups.FirstOrDefault(g => g.name == "Ноги");

                _context.Exercises.AddRange(
                    new Exercise { name = "Жим штанги лежа", description = "Базовое упражнение для грудных мышц. Лягте на скамью, опустите штангу к груди и выжмите вверх.", MuscleGroup = chest, is_custom = false },
                    new Exercise { name = "Отжимания от пола", description = "Универсальное упражнение с собственным весом. Следите за прямой спиной и локтями.", MuscleGroup = chest, is_custom = false },
                    new Exercise { name = "Становая тяга", description = "Одно из лучших базовых упражнений. Тяните штангу за счет ног и спины, держите спину прямой.", MuscleGroup = back, is_custom = false },
                    new Exercise { name = "Приседания со штангой", description = "Король упражнений для ног. Колени не должны выходить за носки, спина прямая.", MuscleGroup = legs, is_custom = false }
                );
                _context.SaveChanges();
            }

            Exercises.Clear();
            var exs = _context.Exercises.Include(e => e.MuscleGroup).ToList();
            foreach (var e in exs) Exercises.Add(e);
        }

        [RelayCommand]
        private void AddExercise()
        {
            if (string.IsNullOrWhiteSpace(NewExerciseName) || SelectedMuscleGroup == null)
            {
                MessageBox.Show("Введите название упражнения и выберите группу мышц!", "Внимание");
                return;
            }

            var newEx = new Exercise
            {
                name = NewExerciseName,
                description = NewExerciseDesc,
                MuscleGroup = SelectedMuscleGroup, // Привязываем объект
                is_custom = true // Это пользовательское упражнение
            };

            _context.Exercises.Add(newEx);
            _context.SaveChanges();

            Exercises.Add(newEx);

            NewExerciseName = string.Empty;
            NewExerciseDesc = string.Empty;
            SelectedMuscleGroup = null;
        }

        [RelayCommand]
        private void DeleteExercise(Exercise ex)
        {
            if (ex == null || !ex.is_custom) return; // Защита от удаления базовых

            var result = MessageBox.Show($"Удалить упражнение '{ex.name}'?", "Подтверждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _context.Exercises.Remove(ex);
                _context.SaveChanges();
                Exercises.Remove(ex);
            }
        }
    }
}