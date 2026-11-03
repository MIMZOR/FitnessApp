using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    public class MuscleGroup
    {
        [Key]
        public int muscle_group_id { get; set; }
        [MaxLength(255)]
        public string name { get; set; } = string.Empty;
        [MaxLength(255)]
        public string description { get; set; } = string.Empty;
    }
}