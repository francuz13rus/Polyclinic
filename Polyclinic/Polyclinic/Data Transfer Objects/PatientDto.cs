namespace Polyclinic.Data_Transfer_Objects
{
    public class PatientDto
    {
        public int IdPatient { get; set; }
        public int IdUser { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool AccountActivated { get; set; }
    }
}
