using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Polyclinic.Models
{
    public class AppointmentRequest
    {
        [Key]
        public int IdRequest { get; set; }

        public int? IdPatient { get; set; }
        public Patient Patient { get; set; }

        public int? IdDoctor { get; set; }
        public Doctor Doctor { get; set; }

        public DateTime RequestDate { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [StringLength(255)]
        public string Reason { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(255)]
        public string DoctorComment { get; set; }
    }
}
