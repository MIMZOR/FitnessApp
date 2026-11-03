using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    public class User
    {
        [Key]
        public int user_id { get; set; }

        [MaxLength(255)]
        public string login { get; set; } = string.Empty;

        [MaxLength(255)]
        public string password_hash { get; set; } = string.Empty;

        [MaxLength(255)]
        public string email { get; set; } = string.Empty;

        [MaxLength(255)]
        public string first_name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string last_name { get; set; } = string.Empty;

        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime updated_at { get; set; } = DateTime.Now;
        public bool is_active { get; set; } = true;

        public ICollection<WeekPlan> WeekPlans { get; set; } = new List<WeekPlan>();
    }
}