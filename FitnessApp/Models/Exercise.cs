using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    public class Exercise
    {
        [Key]
        public int exercise_id { get; set; }
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public int muscle_group_id { get; set; }
        public bool is_custom { get; set; } = true;

        public MuscleGroup? MuscleGroup { get; set; }
    }
}