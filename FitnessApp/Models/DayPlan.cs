using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    public class DayPlan
    {
        [Key]
        public int day_plan_id { get; set; }
        public int week_plan_id { get; set; }
        [MaxLength(7)]
        public string day_of_week { get; set; } = string.Empty; // Например "Monday"
        public DateTime plan_date { get; set; }
        public int muscle_group_id { get; set; }
        [MaxLength(255)]
        public string title { get; set; } = string.Empty;
        [MaxLength(255)]
        public string notes { get; set; } = string.Empty;
        public bool is_rest_day { get; set; }

        public WeekPlan WeekPlan { get; set; }
        public MuscleGroup MuscleGroup { get; set; }
        public ICollection<WorkoutSession> WorkoutSessions { get; set; } = new List<WorkoutSession>();
    }
}