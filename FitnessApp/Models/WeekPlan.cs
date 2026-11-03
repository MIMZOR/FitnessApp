using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    public class WeekPlan
    {
        [Key]
        public int week_plan_id { get; set; }
        public int user_id { get; set; }
        public DateTime week_start_date { get; set; }
        public int week_number { get; set; }
        [MaxLength(255)]
        public string notes { get; set; } = string.Empty;
        public DateTime created_at { get; set; } = DateTime.Now;

        public User User { get; set; }
        public ICollection<DayPlan> DayPlans { get; set; } = new List<DayPlan>();
    }
}