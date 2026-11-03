using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.Models
{
    public class SetLog
    {
        [Key]
        public int set_log_id { get; set; }
        public int workout_session_id { get; set; }
        public int set_number { get; set; }
        public int reps { get; set; }

        [Column(TypeName = "decimal(19,0)")]
        public decimal weight { get; set; }

        public int duration_sec { get; set; }

        [Column(TypeName = "decimal(19,0)")]
        public decimal distance_m { get; set; }

        public TimeSpan completed_at { get; set; }

        [MaxLength(255)]
        public string notes { get; set; } = string.Empty;

        public WorkoutSession WorkoutSession { get; set; }
    }
}