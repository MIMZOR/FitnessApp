using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessApp.Data;
using FitnessApp.Models;
using FitnessApp.Repositories;

namespace FitnessApp.ViewModels
{
    public partial class WorkoutViewModel : ObservableObject
    {
        private readonly SetLogRepository _repository;
        private readonly AppDbContext _context;
        private readonly int _currentSessionId;

        [ObservableProperty] private int _inputWeight;
        [ObservableProperty] private int _inputReps;
        public ObservableCollection<SetLog> SetLogs { get; } = new();

        public WorkoutViewModel(int userId)
        {
            _context = new AppDbContext();
            _repository = new SetLogRepository(_context);

            // Ищем активную сессию или создаем новую
            var session = _context.WorkoutSessions.FirstOrDefault(s => s.user_id == userId && s.status == "Active");

            if (session == null)
            {
                session = new WorkoutSession
                {
                    user_id = userId,
                    session_date = DateTime.Now,
                    started_at = DateTime.Now.TimeOfDay,
                    status = "Active",
                    day_plan_id = 1
                };
                _context.WorkoutSessions.Add(session);
                _context.SaveChanges();
            }

            _currentSessionId = session.workout_session_id;
            LoadSets();
        }

        private void LoadSets()
        {
            SetLogs.Clear();
            var setsFromDb = _repository.GetBySessionId(_currentSessionId);
            foreach (var set in setsFromDb) SetLogs.Add(set);
        }

        [RelayCommand]
        private void SaveSet()
        {
            if (InputWeight <= 0 || InputReps <= 0) return;

            var newSet = new SetLog
            {
                workout_session_id = _currentSessionId,
                set_number = SetLogs.Count + 1,
                weight = InputWeight,
                reps = InputReps,
                completed_at = DateTime.Now.TimeOfDay
            };

            _repository.Save(newSet);
            SetLogs.Add(newSet);
            InputWeight = 0;
            InputReps = 0;
        }
    }
}