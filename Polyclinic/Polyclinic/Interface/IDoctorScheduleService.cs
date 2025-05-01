using Polyclinic.Data_Transfer_Objects;

namespace Polyclinic.Interface
{
    public interface IDoctorScheduleService
    {
        Task<DoctorScheduleDto> GetDoctorScheduleByIdAsync(int id);
        Task<List<DoctorScheduleDto>> GetAllDoctorSchedulesAsync();
        Task<DoctorScheduleDto> CreateDoctorScheduleAsync(DoctorScheduleDto scheduleDto);
        Task<DoctorScheduleDto> UpdateDoctorScheduleAsync(int id, DoctorScheduleDto scheduleDto);
        Task<bool> DeleteDoctorScheduleAsync(int id);
    }
}
