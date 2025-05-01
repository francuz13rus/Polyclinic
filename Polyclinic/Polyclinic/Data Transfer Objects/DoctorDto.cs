namespace Polyclinic.Data_Transfer_Objects
{
    public class DoctorDto
    {
        public int IdDoctor { get; set; }
        public int IdUser { get; set; }
        public string FullName { get; set; }
        public string Specialization { get; set; }
        public string Experience { get; set; }
        public string WorkPhoneNumber { get; set; }
        public string WorkSchedule { get; set; }
        public string PersonalCabinet { get; set; }
    }
}
