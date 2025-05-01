namespace Polyclinic.Data_Transfer_Objects
{
    public class AppointmentRequestDto
    {
        public int IdRequest { get; set; }
        public int IdPatient { get; set; }
        public int IdDoctor { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public string DoctorComment { get; set; }
    }
}
