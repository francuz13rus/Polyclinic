using System.ComponentModel.DataAnnotations;

namespace Polyclinic.Models
{
    public class DoctorSchedule
    {
        [Key]
        public int IdSchedule { get; set; }

        public int? IdDoctor { get; set; }
        public Doctor Doctor { get; set; }

        [Required]
        public DateTime WorkingDay { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }
    }
}
