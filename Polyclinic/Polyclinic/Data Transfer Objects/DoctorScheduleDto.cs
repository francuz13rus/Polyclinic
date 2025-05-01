namespace Polyclinic.Data_Transfer_Objects
{
    public class DoctorScheduleDto
    {
        public int IdSchedule { get; set; }
        public int IdDoctor { get; set; }
        public DateTime WorkingDay { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
