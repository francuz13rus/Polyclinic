using System.ComponentModel.DataAnnotations;

namespace Polyclinic.Models
{
    public class MedicalCard
    {
        [Key]
        public int IdCard { get; set; }

        public int? IdPatient { get; set; }
        public Patient Patient { get; set; }

        public int? IdDoctor { get; set; }
        public Doctor Doctor { get; set; }

        [Required]
        public DateTime DiagnosisDate { get; set; }

        [Required]
        [StringLength(255)]
        public string DiseaseName { get; set; }

        [Required]
        [StringLength(255)]
        public string Treatment { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
