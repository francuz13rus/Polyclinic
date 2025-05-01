namespace Polyclinic.Data_Transfer_Objects
{
    public class MedicalCardDto
    {
        public int IdCard { get; set; }
        public int IdPatient { get; set; }
        public int IdDoctor { get; set; }
        public DateTime DiagnosisDate { get; set; }
        public string DiseaseName { get; set; }
        public string Treatment { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
