using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    public class WorkoutSession
    {
        [Key]
        public int workout_session_id { get; set; }
        public int user_id { get; set; }
        public int day_plan_id { get; set; }
        public DateTime session_date { get; set; }
        public TimeSpan started_at { get; set; } // В БД time(7)
        public TimeSpan finished_at { get; set; }
        [MaxLength(255)]
        public string status { get; set; } = string.Empty;
        [MaxLength(255)]
        public string notes { get; set; } = string.Empty;

        public DayPlan DayPlan { get; set; }
        public ICollection<SetLog> SetLogs { get; set; } = new List<SetLog>();
    }
}