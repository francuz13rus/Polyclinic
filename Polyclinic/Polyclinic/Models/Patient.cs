using System.ComponentModel.DataAnnotations;

namespace Polyclinic.Models
{
    public class Patient
    {
        [Key]
        public int IdPatient { get; set; }

        [Required]
        public int IdUser { get; set; }

        public User User { get; set; } // ✅ Навигационное свойство

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [StringLength(100)]
        public string Gender { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public bool AccountActivated { get; set; }
    }
}
