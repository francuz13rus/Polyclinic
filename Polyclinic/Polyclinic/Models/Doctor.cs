using System.ComponentModel.DataAnnotations;

namespace Polyclinic.Models
{
    public class Doctor
    {
        [Key]
        public int IdDoctor { get; set; }

        public int? IdUser { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [StringLength(100)]
        public string Specialization { get; set; }

        [Required]
        [StringLength(50)]
        public string Experience { get; set; }

        [StringLength(20)]
        public string WorkPhoneNumber { get; set; }

        [StringLength(20)]
        public string WorkSchedule { get; set; }

        [StringLength(100)]
        public string PersonalCabinet { get; set; }
    }
}
