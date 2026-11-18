using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessApp.Data;
using FitnessApp.Models;
using System.Windows;

namespace FitnessApp.ViewModels
{
    public partial class ScheduleViewModel : ObservableObject
    {
        private readonly AppDbContext _context;
        private readonly int _userId;

        public ObservableCollection<DayPlan> WeekDays { get; } = new();
        public ObservableCollection<MuscleGroup> MuscleGroups { get; } = new();

        [ObservableProperty]
        private DayPlan? _selectedDay;

        public ScheduleViewModel(int userId)
        {
            _context = new AppDbContext();
            _userId = userId;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // 1. Проверяем, существует ли пользователь в БД
                var user = _context.Users.Find(_userId);
                if (user == null)
                {
                    MessageBox.Show($"Ошибка: Пользователь с ID {_userId} не найден в базе!\nПожалуйста, зарегистрируйтесь заново.", "Ошибка авторизации");
                    return;
                }

                // 2. Загружаем группы мышц
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
                foreach (var g in groups) MuscleGroups.Add(g);

                // 3. Ищем план на текущую неделю
                var startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
                var weekPlan = _context.WeekPlans.FirstOrDefault(w => w.user_id == _userId && w.week_start_date == startOfWeek);

                if (weekPlan == null)
                {
                    // Используем объект user, чтобы EF Core точно знал, к кому привязывать
                    weekPlan = new WeekPlan { User = user, week_start_date = startOfWeek, week_number = 1 };
                    _context.WeekPlans.Add(weekPlan);
                    _context.SaveChanges();

                    string[] days = { "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс" };
                    for (int i = 0; i < 7; i++)
                    {
                        _context.DayPlans.Add(new DayPlan
                        {
                            week_plan_id = weekPlan.week_plan_id,
                            day_of_week = days[i],
                            plan_date = startOfWeek.AddDays(i)
                        });
                    }
                    _context.SaveChanges();
                }

                var plans = _context.DayPlans.Where(d => d.week_plan_id == weekPlan.week_plan_id).OrderBy(d => d.plan_date).ToList();
                foreach (var p in plans) WeekDays.Add(p);

                SelectedDay = WeekDays.FirstOrDefault();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка БД: {ex.Message}\n\nВнутренняя ошибка: {ex.InnerException?.Message}", "Критическая ошибка");
            }
        }

        [RelayCommand]
        private void SaveDayPlan()
        {
            if (SelectedDay == null) return;

            try
            {
                _context.DayPlans.Update(SelectedDay);
                _context.SaveChanges();
                MessageBox.Show("План на день сохранен!", "Успех");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.InnerException?.Message ?? ex.Message}", "Ошибка");
            }
        }
    }
}