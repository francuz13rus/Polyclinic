using System.ComponentModel.DataAnnotations;

namespace Polyclinic.Models
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; } // Например, "Patient" или "Doctor"

        public List<Patient> Patients { get; set; } // Добавлено для связи с Patient
    }
}